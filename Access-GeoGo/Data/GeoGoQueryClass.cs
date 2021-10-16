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
        private FuelTransPage GGP;

        /// <summary>
        /// Database params window (<see cref=DBParamsPage"/>) instance
        /// </summary>
        private DBParamsPage DBP;

        /// <summary>
        /// Connection string for MS Access fuel database - set by <see cref="DBParamsPage.DBFileNameBox"/>
        /// </summary>
        private string ConStr;

        /// <summary>
        /// Cancellation Token
        /// </summary>
        private CancellationToken CT;

        /// <summary>
        /// Results <see cref="DataTable"/>, becomes DataSource for <see cref="FuelTransPage.GeoGoDataView"/>
        /// </summary>
        private DataTable GeoGoTable;

        /// <summary>
        /// Authenticated <see cref="Geotab.GeotabAPI"/> instance
        /// </summary>
        private static GeotabAPI GeotabAPI;

        //Private Variables

        #region Results Calls & Lists; Device & GeoGo Entry Lists

        /// <summary>
        /// <see cref="Array"/> of <see cref="DataRow"/> entries from Access Fuel DB
        /// </summary>
        private DataRow[] AccessDBEntries;

        /// <summary>
        /// Contains calls to get Odometer readings from Geotab via <see cref="StatusData"/>
        /// </summary>
        private GeotabAPI.MultiCallList<StatusData> odometerCalls;

        /// <summary>
        /// Contains calls to get Engine Hours readings from Geotab via <see cref="StatusData"/>
        /// </summary>
        private GeotabAPI.MultiCallList<StatusData> engineHoursCalls;

        /// <summary>
        /// Contains calls to get Location readings from Geotab via <see cref="LogRecord"/>
        /// </summary>
        private GeotabAPI.MultiCallList<LogRecord> locationCalls;

        /// <summary>
        /// Contains calls to get the <see cref="Driver"/> of a <see cref="Device"/> from Geotab via <see cref="DriverChange"/>
        /// </summary>
        private GeotabAPI.MultiCallList<DriverChange> driverCalls;

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

        private static List<DeviceEntry> DeviceEntriesList;
        private static List<DBEntry> DeviceErrorList;
        private static List<GeoGo_Entry> GeoGoEntries;
        public bool IsComplete;
        private int RecordsCt;

        #endregion Results Calls & Lists; Device & GeoGo Entry Lists

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Use new <see cref="CancellationToken"/>
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/></param>
        public void UpdateCT(CancellationToken ct)
        {
            CT = ct;
            GeotabAPI = new GeotabAPI(Program.API, ct);
        }

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
            if (!disposed)// Check to see if Dispose has already been called.
            {
                // If disposing equals true, dispose all managed and unmanaged resources
                if (disposing) GeotabAPI.Dispose(); // Dispose managed resources.

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.
                GGP = null;
                DBP = null;
                ConStr = null;
                GeoGoTable = null;
                AccessDBEntries = null;
                odometerCalls = null;
                engineHoursCalls = null;
                locationCalls = null;
                driverCalls = null;
                _deviceCache = null;
                _deviceNameCache = null;
                _driverCache = null;
                DeviceEntriesList = null;
                DeviceErrorList = null;
                GeoGoEntries = null;

                disposed = true;// Note disposing has been done.
            }
        }

        /// <summary>
        /// Initializes a <see cref="GeoGoQuery"/> instance
        /// </summary>
        /// <param name="GeoGoPage"><see cref="FuelTransPage"/> instanced, assigned to <see cref="GGP"/> & contains <see cref="DBP"/></param>
        /// <param name="ct">Cancellation Token, assigned to <see cref="CT"/></param>
        public GeoGoQuery(FuelTransPage GeoGoPage, CancellationToken ct)
        {
            GGP = GeoGoPage;
            DBP = GeoGoPage.DBP;
            CT = ct;
            GeotabAPI = new GeotabAPI(Program.API, ct);
            ConStr = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + DBP.File + "; Persist Security Info = False";
        }

        /// <summary>
        /// Gets Geotab query call results & displays them - Executes all functions/procedures
        /// </summary>
        public async Task GeoGoQueryAsync()
        {
            Stopwatch watch = Stopwatch.StartNew();
            try
            {
                Task prep = PrepUIPage();
                Task devCache = GetDeviceCache();
                Task drvrCache = GetDriverCache();
                Task getDB = GetDBEntries();
                await Task.WhenAll(prep, devCache, drvrCache, getDB);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                await GetDeviceEntryList();
                await GetCallsList();
                await GetApiResults();
                await GetGeoGoTable();

                if (CT.IsCancellationRequested)
                {
                    Dispose();
                    return;
                }
                GGP.GeoGoDataView.DataSource = GeoGoTable;
                GGP.ResultsFoundLabel.Text = $"Results Found: {GeoGoTable.Rows.Count} out of {DBP.Limit}";
                watch.Stop();
                TimeSpan ts = watch.Elapsed;
                string hours = ts.Hours > 0 ? $"{ts.Hours:00}:" : "";
                string elapsedTime = $"{hours}{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
                await UpdateProgress(100, $"Done | Run Time: {elapsedTime}");
                MessageBox.Show($"Records Found: {RecordsCt}\nSuccess: {DeviceEntriesList.Count}\nError: {DeviceErrorList.Count}", "Query Complete");
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
            return;
        }

        /// <summary>
        /// Update <see cref="FuelTransPage.QueryStatusBar"/> progress
        /// </summary>
        /// <param name="val">New <see cref="FuelTransPage.QueryLoadingBar"/> percentage value</param>
        /// <param name="stat">New <see cref="FuelTransPage.QueryLoadingLabel"/> status message</param>
        private Task UpdateProgress(int val, string stat = "Loading...")
        {
            if (CT.IsCancellationRequested) return Task.CompletedTask;
            decimal percentage = Convert.ToDecimal(val) / 100;
            decimal value = Math.Round(Convert.ToDecimal(GGP.QueryLoadingBar.Maximum) * percentage);
            if (value != 0 && value < GGP.QueryLoadingBar.Value) return Task.CompletedTask;
            GGP.QueryLoadingBar.Value = (int)value;
            GGP.QueryLoadingLabel.Text = stat;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Set/reset <see cref="FuelTransPage"/> instance (<see cref="GGP"/>) UI to prep for results
        /// </summary>
        private async Task PrepUIPage()
        {
            CT.ThrowIfCancellationRequested();
            if (GeoGoTable is null) GeoGoTable = new DataTable();
            else
            {
                GeoGoTable.Clear();
                GGP.GeoGoDataView.DataSource = GeoGoTable;
            }
            GGP.QueryLoadingLabel.Visible = true;
            GGP.QueryLoadingBar.Visible = true;
            GGP.ResultsFoundLabel.Text = "Results Found:";
            GGP.InsertBtn.Enabled = true;
            await UpdateProgress(0);
        }

        private async Task GetDeviceCache()
        {
            _deviceCache = await GeotabAPI.GetDictionary<Device, Id>(d => d.Id);
            _deviceNameCache = _deviceCache.ToDictionary(d => d.Value.Name, d => d.Key);
            await UpdateProgress(40);
        }

        private async Task GetDriverCache()
        {
            _driverCache = await GeotabAPI.GetDictionary<User, Id>(d => d.Id);
            await UpdateProgress(40);
        }

        private async Task GetDBEntries()
        {
            CT.ThrowIfCancellationRequested();
            DataTable AccessDT = new DataTable();
            OleDbConnection con = new OleDbConnection(ConStr);
            string query = $"SELECT TOP {DBP.Limit} * FROM [{DBP.Table}] WHERE [{DBP.GTStatus}] = '{DBP.GTS_Value}';";
            using (OleDbCommand cmd = new OleDbCommand(query, con))
            {
                try
                {
                    con.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();
                    AccessDT.Load(reader);
                    con.Close();
                }
                catch (Exception err)
                {
                    Program.ShowError(err);
                    con.Close();
                }
            }
            AccessDBEntries = AccessDT.Select();
            RecordsCt = AccessDBEntries.Length;
            await UpdateProgress(15);
            return;
        }

        private async Task GetDeviceEntryList()
        {
            CT.ThrowIfCancellationRequested();
            DeviceEntriesList = new List<DeviceEntry>();
            DeviceErrorList = new List<DBEntry>();
            string id = DBP.Id;
            string time = DBP.Time;
            foreach ((string devName, DBEntry dbEntry) in from DataRow row in AccessDBEntries
                                               let devNameField = row.Field<string>(DBP.Vehicle) ?? ""// Get Vehicle field; if null, set to ""
                                               let devName = Regex.Replace(devNameField, @"\s\d*", "")//Remove vehicle name suffix that matches the pattern space#* (e.g. 'Sara 04' to 'Sara')
                                               let dbEntry = new DBEntry(new DBEntryParams
                                               {
                                                   Id = row.Field<int>(id),
                                                   Vehicle = devName,
                                                   Timestamp = row.Field<DateTime>(time).ToUniversalTime()
                                               })
                                               select (devName, dbEntry))
            {
                if (_deviceNameCache.TryGetValue(devName, out Id devId))
                    DeviceEntriesList.Add(new DeviceEntry(dbEntry, _deviceCache[devId]));
                else
                    DeviceErrorList.Add(dbEntry);
            }

            await UpdateProgress(45);
        }

        private async Task GetCallsList()
        {
            if (CT.IsCancellationRequested) return;
            odometerCalls = new GeotabAPI.MultiCallList<StatusData>();
            engineHoursCalls = new GeotabAPI.MultiCallList<StatusData>();
            locationCalls = new GeotabAPI.MultiCallList<LogRecord>();
            driverCalls = new GeotabAPI.MultiCallList<DriverChange>();
            foreach ((Device device, DBEntry entry) in from DeviceEntry deviceEntry in DeviceEntriesList
                                            let device = deviceEntry.Device
                                            let entry = deviceEntry.Entry
                                            select (device, entry))
            {
                odometerCalls.AddCall("Get", new
                {
                    search = new StatusDataSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DiagnosticSearch = new DiagnosticSearch(KnownId.DiagnosticOdometerAdjustmentId),
                        DeviceSearch = new DeviceSearch(device.Id)
                    }
                });
                engineHoursCalls.AddCall("Get", new
                {
                    search = new StatusDataSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DiagnosticSearch = new DiagnosticSearch(KnownId.DiagnosticEngineHoursAdjustmentId),
                        DeviceSearch = new DeviceSearch(device.Id)
                    }
                });
                locationCalls.AddCall("Get", new
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
                driverCalls.AddCall("Get", new
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
            return;
        }

        private async Task GetApiResults()
        {
            try
            {
                await Task.WhenAll(odometerCalls.MakeCall(), engineHoursCalls.MakeCall(), locationCalls.MakeCall(), driverCalls.MakeCall());
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            if (CT.IsCancellationRequested) return;
            List<StatusData> oCallResults = odometerCalls.GetResults();
            List<StatusData> eHoursCallResults = engineHoursCalls.GetResults();
            List<LogRecord> lCallResults = locationCalls.GetResults();
            List<DriverChange> dCallResults = driverCalls.GetResults();
            await UpdateProgress(75);

            GeoGoEntries = new List<GeoGo_Entry>();
            for (int i = 0; i < oCallResults.Count; i++)
            {
                StatusData odometer = oCallResults[i];
                StatusData engineHours = eHoursCallResults[i];
                LogRecord location = lCallResults[i];
                string driver = (dCallResults[i] != null) && _driverCache.TryGetValue(dCallResults[i].GetDriverId(), out User user) ? user.Name : "Unknown Driver";
                Device device = DeviceEntriesList[i].Device;
                DBEntry entry = DeviceEntriesList[i].Entry;
                GeoGoEntries.Add(new GeoGo_Entry(new GeoGo_EntryData
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
            return;
        }

        private async Task GetGeoGoTable()
        {
            if (CT.IsCancellationRequested) return;
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
            foreach ((GeoGo_Entry GeoGo, DataRow dr) in from GeoGo_Entry GeoGo in GeoGoEntries
                                        let dr = dt.NewRow()
                                        select (GeoGo, dr))
            {
                dr[0] = GeoGo.EntryID;
                dr[1] = GeoGo.GTStatus;
                dr[2] = GeoGo.Timestamp.ToLocalTime();
                dr[3] = GeoGo.DeviceName;
                dr[4] = GeoGo.Miles;
                dr[5] = GeoGo.Hours;
                dr[6] = GeoGo.Latitude;
                dr[7] = GeoGo.Longitude;
                dr[8] = GeoGo.Driver;
                dt.Rows.Add(dr);
            }

            foreach ((DBEntry dbe, DataRow dr) in from DBEntry dbe in DeviceErrorList
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

            GeoGoTable = dt;
            await UpdateProgress(100, "Done");
            return;
        }

        public void UpdateAccessDB()
        {
            if (CT.IsCancellationRequested) return;
            DataTable dt = GeoGoTable;
            OleDbConnection con = new OleDbConnection(ConStr);
            using (con)
            {
                int updated = 0;
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
                                          let query = $"UPDATE [{DBP.Table}] SET [{DBP.GTStatus}] = '{gtStatus}', [{DBP.Odometer}] = {oReading}, [{DBP.EngineHrs}] = {engineHrs}, [{DBP.Latitude}] = '{latitude}', [{DBP.Longitude}] = '{longitude}', [{DBP.Driver}] = '{driver}' WHERE [{DBP.Id}] = {entryId};"
                                          select query)
                        using (OleDbCommand cmd = new OleDbCommand(query, con, transaction))
                        {
                            updated += cmd.ExecuteNonQuery();
                        }

                    transaction.Commit();
                }
                catch (Exception err)
                {
                    Program.ShowError(err);
                }
                finally
                {
                    if (!GGP.IsDisposed) GGP.InsertBtn.Enabled = false;
                    MessageBox.Show($"{updated} out of {dt.Rows.Count} entries updated successfully.", "DB Update Complete");
                }
            }
        }
    }
}