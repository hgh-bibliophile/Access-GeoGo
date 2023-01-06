using Access_GeoGo.Data.Geotab;
using Access_GeoGo.Forms;
using Geotab.Checkmate.ObjectModel;
using Geotab.Checkmate.ObjectModel.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Access_GeoGo.Data
{
    internal class GeoGoQuery : IDisposable
    {
        /// <summary>
        /// Query results window (<see cref="FuelTransPage"/>) instance
        /// </summary>
        private FuelTransPage _ggp;

        /// <summary>
        /// Database params window (<see cref=DBParamsPage"/>) instance
        /// </summary>
        private DbParamsPage _dbp;

        /// <summary>
        /// Connection string for MS Access fuel database - set by <see cref="DbParamsPage.DBFileNameBox"/>
        /// </summary>
        private string _conStr;

        /// <summary>
        /// Cancellation Token
        /// </summary>
        private CancellationToken _ct;

        /// <summary>
        /// Results <see cref="DataTable"/>, becomes DataSource for <see cref="FuelTransPage.GeoGoDataView"/>
        /// </summary>
        private DataTable _geoGoTable;

        /// <summary>
        /// Authenticated <see cref="GeotabApi"/> instance
        /// </summary>
        private readonly GeotabApi _geotabApi;

        //Private Variables

        #region Results Calls & Lists; Device & GeoGo Entry Lists

        /// <summary>
        /// <see cref="Array"/> of <see cref="DataRow"/> entries from Access Fuel DB
        /// </summary>
        private DataRow[] _accessDbEntries;

        /// <summary>
        /// Contains calls to get Odometer readings from Geotab via <see cref="StatusData"/>
        /// </summary>
        private GeotabApi.MultiCallList<StatusData> _odometerCalls;

        /// <summary>
        /// Contains calls to get Engine Hours readings from Geotab via <see cref="StatusData"/>
        /// </summary>
        private GeotabApi.MultiCallList<StatusData> _engineHoursCalls;

        /// <summary>
        /// Contains calls to get Location readings from Geotab via <see cref="LogRecord"/>
        /// </summary>
        private GeotabApi.MultiCallList<LogRecord> _locationCalls;

        /// <summary>
        /// Contains calls to get the <see cref="Driver"/> of a <see cref="Device"/> from Geotab via <see cref="DriverChange"/>
        /// </summary>
        private GeotabApi.MultiCallList<DriverChange> _driverCalls;

        /// <summary>
        /// Cache of <see cref="Device"/> by <see cref="Id"/>
        /// </summary>
        private static Dictionary<Id, Device> _deviceCache;

        /// <summary>
        /// Cache of <see cref="Id"/> by name
        /// </summary>
        private static Dictionary<string, Id> _deviceNameCache;

        /// <summary>
        /// Cache of <see cref="User"/> by <see cref="Id"/>
        /// </summary>
        private static Dictionary<Id, User> _driverCache;

        private List<DeviceEntry> _deviceEntriesList;
        private List<DbEntry> _deviceErrorList;
        private List<GeoGoEntry> _geoGoEntries;
        public bool IsComplete;
        public bool DbUpdated;
        private int _recordsCt;

        #endregion Results Calls & Lists; Device & GeoGo Entry Lists

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Use new <see cref="CancellationToken"/>
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/></param>
        public void UpdateCt(CancellationToken ct) => _geotabApi.UpdateCt(_ct = ct);

        /*Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.*/

        /// <summary>
        /// Disposes of managed & unmanaged resources
        /// </summary>
        /// <param name="disposing">True: called directly/indirectly by user's code; False: called by the runtime from inside the finalizer</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;// Check to see if Dispose has already been called.
            // If disposing equals true, dispose all managed and unmanaged resources
            if (disposing) _geotabApi.Dispose(); // Dispose managed resources.

            // Call the appropriate methods to clean up unmanaged resources here.
            // If disposing is false, only the following code is executed.
            _ggp = null;
            _dbp = null;
            _conStr = null;
            _geoGoTable = null;
            _accessDbEntries = null;
            _odometerCalls = null;
            _engineHoursCalls = null;
            _locationCalls = null;
            _driverCalls = null;
            _deviceCache = null;
            _deviceNameCache = null;
            _driverCache = null;
            _deviceEntriesList = null;
            _deviceErrorList = null;
            _geoGoEntries = null;

            _disposed = true;// Note disposing has been done.
        }

        /// <summary>
        /// Initializes a <see cref="GeoGoQuery"/> instance
        /// </summary>
        /// <param name="geoGoPage"><see cref="FuelTransPage"/> instanced, assigned to <see cref="_ggp"/> & contains <see cref="_dbp"/></param>
        /// <param name="ct">Cancellation Token, assigned to <see cref="_ct"/></param>
        public GeoGoQuery(FuelTransPage geoGoPage, CancellationToken ct)
        {
            _ggp = geoGoPage;
            _dbp = geoGoPage.Dbp;
            _geotabApi = new GeotabApi(_ct = ct);
            _conStr = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + _dbp.File + "; Persist Security Info = False";
        }

        /// <summary>
        /// Gets Geotab query call results & displays them - Executes all functions/procedures
        /// </summary>
        public async Task GeoGoQueryAsync()
        {
            Stopwatch watch = Stopwatch.StartNew();
            try
            {
                await Task.WhenAll(PrepUiPage(), GetDeviceCache(), GetDriverCache(), GetDbEntries());
                GC.Collect();
                GC.WaitForPendingFinalizers();
                await GetDeviceEntryList();
                await GetCallsList();
                await GetApiResults();
                await GetGeoGoTable();

                if (_ct.IsCancellationRequested)
                {
                    Dispose();
                    return;
                }
                _ggp.GeoGoDataView.DataSource = _geoGoTable;
                _ggp.ResultsFoundLabel.Text = $"Results Found: {_geoGoTable.Rows.Count} out of {_dbp.Limit}";
                watch.Stop();
                TimeSpan ts = watch.Elapsed;
                string hours = ts.Hours > 0 ? $"{ts.Hours:00}:" : "";
                string elapsedTime = $"{hours}{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
                await UpdateProgress(100, $"Done | Run Time: {elapsedTime}");

                string msg = $"Records Found: {_recordsCt}\nSuccess: {_deviceEntriesList.Count}\nError: {_deviceErrorList.Count}";
                Cleanup();

                MessageBox.Show(msg, "Query Complete");
                IsComplete = true;
            }
            catch (OperationCanceledException)
            {
                watch.Stop();
                throw;
            }
            catch (Exception err)
            {
                watch.Stop();
                Program.ShowError(err);
            }
        }

        private void Cleanup()
        {
            _accessDbEntries = null;
            _odometerCalls = null;
            _engineHoursCalls = null;
            _locationCalls = null;
            _driverCalls = null;
            _deviceEntriesList = null;
            _deviceErrorList = null;
            _geoGoEntries = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Update <see cref="FuelTransPage.QueryStatusBar"/> progress
        /// </summary>
        /// <param name="val">New <see cref="FuelTransPage.QueryLoadingBar"/> percentage value</param>
        /// <param name="stat">New <see cref="FuelTransPage.QueryLoadingLabel"/> status message</param>
        private Task UpdateProgress(int val, string stat = "Loading...")
        {
            if (_ct.IsCancellationRequested) return Task.CompletedTask;
            decimal percentage = Convert.ToDecimal(val) / 100;
            decimal value = Math.Round(Convert.ToDecimal(_ggp.QueryLoadingBar.Maximum) * percentage);
            if (value != 0 && value < _ggp.QueryLoadingBar.Value) return Task.CompletedTask;
            _ggp.QueryLoadingBar.Value = (int)value;
            _ggp.QueryLoadingLabel.Text = stat;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Set/reset <see cref="FuelTransPage"/> instance (<see cref="_ggp"/>) UI to prep for results
        /// </summary>
        private async Task PrepUiPage()
        {
            _ct.ThrowIfCancellationRequested();
            if (_geoGoTable is null) _geoGoTable = new DataTable();
            else
            {
                _geoGoTable.Clear();
                _ggp.GeoGoDataView.DataSource = _geoGoTable;
            }
            _ggp.QueryLoadingLabel.Visible = true;
            _ggp.QueryLoadingBar.Visible = true;
            _ggp.ResultsFoundLabel.Text = "Results Found:";
            _ggp.InsertBtn.Enabled = true;
            await UpdateProgress(0);
        }

        private async Task GetDeviceCache()
        {
            if (_deviceCache == null) _deviceCache = await _geotabApi.GetDictionary<Device, Id>(d => d.Id);
            if (_deviceNameCache == null) _deviceNameCache = _deviceCache.ToDictionary(d => d.Value.Name, d => d.Key);
            await UpdateProgress(40);
        }

        private async Task GetDriverCache()
        {
            if (_driverCache == null) _driverCache = await _geotabApi.GetDictionary<User, Id>(d => d.Id);
            await UpdateProgress(35);
        }

        private async Task GetDbEntries()
        {
            _ct.ThrowIfCancellationRequested();
            DataTable accessDt = new DataTable();
            OleDbConnection con = new OleDbConnection(_conStr);
            string query = $"SELECT TOP {_dbp.Limit} * FROM [{_dbp.Table}] WHERE [{_dbp.GtStatus}] = '{_dbp.GtsValue}';";
            using (OleDbCommand cmd = new OleDbCommand(query, con))
            {
                try
                {
                    con.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();
                    accessDt.Load(reader);
                    con.Close();
                }
                catch (Exception err)
                {
                    Program.ShowError(err);
                    con.Close();
                }
            }
            _accessDbEntries = accessDt.Select();
            _recordsCt = _accessDbEntries.Length;
            await UpdateProgress(15);
        }

        private async Task GetDeviceEntryList()
        {
            _ct.ThrowIfCancellationRequested();
            _deviceEntriesList = new List<DeviceEntry>();
            _deviceErrorList = new List<DbEntry>();
            string id = _dbp.Id;
            string time = _dbp.Time;
            foreach ((string devName, DbEntry dbEntry) in from DataRow row in _accessDbEntries
                    let devNameField = row.Field<string>(_dbp.Vehicle) ?? ""// Get Vehicle field; if null, set to ""
                    let devName = Regex.Replace(devNameField, @"\s\d*", "")//Remove vehicle name suffix that matches the pattern space#* (e.g. 'Sara 04' to 'Sara')
                    let dbEntry = new DbEntry(new DbEntryParams
                    {
                        Id = row.Field<int>(id),
                        Vehicle = devName,
                        Timestamp = row.Field<DateTime>(time).ToUniversalTime()
                    })
                    select (devName, dbEntry))
            {
                if (_deviceNameCache.TryGetValue(devName, out Id devId))
                    _deviceEntriesList.Add(new DeviceEntry(dbEntry, _deviceCache[devId]));
                else
                    _deviceErrorList.Add(dbEntry);
            }

            await UpdateProgress(45);
        }

        private async Task GetCallsList()
        {
            if (_ct.IsCancellationRequested) return;
            _odometerCalls = new GeotabApi.MultiCallList<StatusData>();
            _engineHoursCalls = new GeotabApi.MultiCallList<StatusData>();
            _locationCalls = new GeotabApi.MultiCallList<LogRecord>();
            _driverCalls = new GeotabApi.MultiCallList<DriverChange>();
            foreach ((Device device, DbEntry entry) in from DeviceEntry deviceEntry in _deviceEntriesList
                    let device = deviceEntry.Device
                    let entry = deviceEntry.Entry
                    select (device, entry))
            {
                _odometerCalls.AddCall("Get", new
                {
                    search = new StatusDataSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DiagnosticSearch = new DiagnosticSearch(KnownId.DiagnosticOdometerAdjustmentId),
                        DeviceSearch = new DeviceSearch(device.Id)
                    }
                });
                _engineHoursCalls.AddCall("Get", new
                {
                    search = new StatusDataSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DiagnosticSearch = new DiagnosticSearch(KnownId.DiagnosticEngineHoursAdjustmentId),
                        DeviceSearch = new DeviceSearch(device.Id)
                    }
                });
                _locationCalls.AddCall("Get", new
                {
                    search = new LogRecordSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DeviceSearch = new DeviceSearch
                        {
                            Id = device.Id
                        }
                    }
                });
                _driverCalls.AddCall("Get", new
                {
                    search = new DriverChangeSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DeviceSearch = new DeviceSearch
                        {
                            Id = device.Id
                        },
                        IncludeOverlappedChanges = true
                    }
                });
            }

            await UpdateProgress(50);
        }

        private async Task GetApiResults()
        {
            await Task.WhenAll(_odometerCalls.Execute(), _engineHoursCalls.Execute(), _locationCalls.Execute(), _driverCalls.Execute());
            if (_ct.IsCancellationRequested) return;
            List<StatusData> oCallResults = _odometerCalls.GetResults();
            List<StatusData> eHoursCallResults = _engineHoursCalls.GetResults();
            List<LogRecord> lCallResults = _locationCalls.GetResults();
            List<DriverChange> dCallResults = _driverCalls.GetResults();
            await UpdateProgress(75);

            _geoGoEntries = new List<GeoGoEntry>();
            for (int i = 0; i < oCallResults.Count; i++)
            {
                StatusData odometer = oCallResults[i];
                StatusData engineHours = eHoursCallResults[i];
                LogRecord location = lCallResults[i];
                string driver = dCallResults[i] != null && _driverCache.TryGetValue(dCallResults[i].GetDriverId(), out User user) ? user.Name : "Unknown Driver";
                Device device = _deviceEntriesList[i].Device;
                DbEntry entry = _deviceEntriesList[i].Entry;
                _geoGoEntries.Add(new GeoGoEntry(new GeoGoEntryData
                {
                    Device = device,
                    DeviceEngineHours = engineHours,
                    DeviceMileage = odometer,
                    DeviceLocation = location,
                    Driver = driver,
                    Entry = entry
                }));
            }
            await UpdateProgress(90);
        }

        private async Task GetGeoGoTable()
        {
            if (_ct.IsCancellationRequested) return;
            DataTable dt = new DataTable();
            dt.Columns.Add("Entry ID");
            dt.Columns.Add("Geotab Status");
            dt.Columns.Add("Time");
            dt.Columns.Add("Deivce Name");
            dt.Columns.Add("Odometer");
            dt.Columns.Add("Engine Hours");
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");
            dt.Columns.Add("Driver");
            foreach ((GeoGoEntry geoGo, DataRow dr) in from GeoGoEntry geoGo in _geoGoEntries
                    let dr = dt.NewRow()
                    select (geoGo, dr))
            {
                dr[0] = geoGo.EntryId;
                dr[1] = geoGo.GtStatus;
                dr[2] = geoGo.Timestamp.ToLocalTime();
                dr[3] = geoGo.DeviceName;
                dr[4] = geoGo.Miles;
                dr[5] = geoGo.Hours;
                dr[6] = geoGo.Latitude;
                dr[7] = geoGo.Longitude;
                dr[8] = geoGo.Driver;
                dt.Rows.Add(dr);
            }

            foreach ((DbEntry dbe, DataRow dr) in from DbEntry dbe in _deviceErrorList
                    let dr = dt.NewRow()
                    select (dbe, dr))
            {
                dr[0] = dbe.Id;
                dr[1] = "GTNoDevice";
                dr[2] = dbe.Timestamp;
                dr[3] = dbe.Vehicle;
                dr[4] = null;
                dr[5] = null;
                dr[6] = null;
                dr[7] = null;
                dr[8] = null;
                dt.Rows.Add(dr);
            }

            _geoGoTable = dt;
            await UpdateProgress(99);
        }

        public void UpdateAccessDb()
        {
            if (_ct.IsCancellationRequested) return;
            DataTable dt = _geoGoTable;
            OleDbConnection con = new OleDbConnection(_conStr);
            using (con)
            {
                int updated = 0;
                int errors = 0;
                try
                {
                    con.Open();
                    OleDbTransaction transaction = con.BeginTransaction();
                    foreach (string query in from DataRow dr in dt.Rows
                            let gtStatus = dr.Field<string>("Geotab Status")
                            let oReading = dr.Field<string>("Odometer") ?? "NULL"
                            let engineHrs = dr.Field<string>("Engine Hours") ?? "NULL"
                            let latitude = dr.Field<string>("Latitude")
                            let longitude = dr.Field<string>("Longitude")
                            let driver = dr.Field<string>("Driver")
                            let entryId = dr.Field<string>("Entry ID")
                            let query = $"UPDATE [{_dbp.Table}] SET [{_dbp.GtStatus}] = '{gtStatus}', " +
                                        $"[{_dbp.Odometer}] = {oReading}, [{_dbp.EngineHrs}] = {engineHrs}, " +
                                        $"[{_dbp.Latitude}] = '{latitude}', [{_dbp.Longitude}] = '{longitude}', " +
                                        $"[{_dbp.Driver}] = '{driver}' WHERE [{_dbp.Id}] = {entryId};"
                            select query)
                        using (OleDbCommand cmd = new OleDbCommand(query, con, transaction))
                        {
                            try
                            {
                                updated += cmd.ExecuteNonQuery();
                            }
                            catch (InvalidOperationException)
                            {
                                errors++;
                            }
                        }
                    transaction.Commit();
                }
                catch (Exception err)
                {
                    Program.ShowError(err);
                }
                finally
                {
                    con.Close();
                    if (!_ggp.IsDisposed) _ggp.InsertBtn.Enabled = false;
                    DbUpdated = true;
                    MessageBox.Show($"Total: {dt.Rows.Count}\nSuccessful: {updated}\nErrors: {errors}", "DB Update Complete");
                }
            }
        }
    }
}