using Geotab.Checkmate.ObjectModel;
using Geotab.Checkmate.ObjectModel.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Access_GeoGo.Forms;
using System.Threading;

namespace Access_GeoGo.Data
{
    class GeoGoQueryOld : IDisposable
    {
        private FuelTransPage GGP;
        private DBParamsPage DBP;
        private string ConStr;
        private CancellationToken CT;
        private DataTable GeoGoTable = null;
        //Private Variables
        #region Results Calls & Lists; Device & GeoGo Entry Lists
        private DataRow[] AccessDBEntries;
        private List<object> odometerCalls;
        private List<object> engineHoursCalls;
        private List<object> locationCalls;

        static Dictionary<Id, Device> _deviceCache;
        static Dictionary<string, Id> _deviceNameCache;

        static List<DeviceEntry> DeviceEntriesList;
        static List<DBEntry> DeviceErrorList;
        static List<GeoGo_Entry> GeoGoEntries;
        public static bool IsComplete = false;
        private int RecordsCt;
        #endregion
        private bool disposed = false;

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    //component.Dispose();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.


                // Note disposing has been done.
                disposed = true;
            }
        }
        public GeoGoQueryOld(FuelTransPage GeoGoPage)
        {
            GGP = GeoGoPage;
            DBP = GeoGoPage.DBP;
            ConStr = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + DBP.File + "; Persist Security Info = False";
        }
        public async Task CancelCleanupAsync()
        {
            while (!IsComplete)
            {
                await Task.Delay(250);
            }
            GGP = null;
            DBP = null;
            ConStr = null;
            GeoGoTable = null;
            AccessDBEntries = null;
            odometerCalls = null;
            engineHoursCalls = null;
            locationCalls = null;
            _deviceCache = null;
            _deviceNameCache = null;
            DeviceEntriesList = null;
            DeviceErrorList = null;
            GeoGoEntries = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public async Task GeoGoQueryAsync(CancellationToken ct)
        {
            CT = ct;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                var prep = PrepUIPage();
                var devCache = GetDeviceCache(ct);
                var getDB = GetDBEntries();
                await Task.WhenAll(prep, devCache, getDB);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                await GetDeviceEntryList();
                await GetCallsList(ct);
                await GetApiResults(ct);
                await GetGeoGoTable(); 
                GGP.GeoGoDataView.DataSource = GeoGoTable;
                GGP.ResultsFoundLabel.Text = $"Results Found: {GeoGoTable.Rows.Count} out of {DBP.Limit}";
                watch.Stop();
                TimeSpan ts = watch.Elapsed;
                string hours = ts.Hours > 0 ? $"{ts.Hours:00}:" : "";
                string elapsedTime = $"{hours}{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
                await UpdateProgress(100, $"Done | Run Time: {elapsedTime}");
                MessageBox.Show($"Records Found: {RecordsCt}\nSuccess: {DeviceEntriesList.Count}\nError: {DeviceErrorList.Count}", "Query Complete");
            }
            catch (OperationCanceledException e)
            {
                watch.Stop();
                IsComplete = true;
                Program.ShowError(e);
                throw;
            }
            catch (Exception err)
            {
                watch.Stop();
                IsComplete = true;
                Program.ShowError(err);
            }
            return;
        }
        private async Task UpdateProgress(int val, string stat = "Loading...")
        {
            if (CT.IsCancellationRequested) return;
            decimal percentage = Convert.ToDecimal(val) / 100;
            decimal value = Math.Round(Convert.ToDecimal(GGP.QueryLoadingBar.Maximum) * percentage);
            if (value < GGP.QueryLoadingBar.Value) return;
            GGP.QueryLoadingBar.Value = (int)value;
            GGP.QueryLoadingLabel.Text = stat;
        }
        public async Task PrepUIPage()
        {
            CT.ThrowIfCancellationRequested();
            if (GeoGoTable == null)
                GeoGoTable = new DataTable();
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
        public async Task GetDeviceCache(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            List<Device> devices = await Program.API.CallAsync<List<Device>>("Get", typeof(Device), new { search = new DeviceSearch { ToDate = DateTime.Now } }, ct);
            _deviceNameCache = devices.ToDictionary(d => d.Name, d => d.Id);
            _deviceCache = devices.ToDictionary(d => d.Id);
            var _dev = _deviceCache.ToDictionary(d => d.Value.Name, d => d.Key);
            await UpdateProgress(40);
        }
        public async Task GetDBEntries()
        {
            CT.ThrowIfCancellationRequested();
            DataTable AccessDT = new DataTable();
            OleDbConnection con = new OleDbConnection(ConStr);
            OleDbDataReader reader;
            string query = $"SELECT TOP {DBP.Limit} * FROM [{DBP.Table}] WHERE [{DBP.Odometer}] = 0;";
            using (OleDbCommand cmd = new OleDbCommand(query, con))
            {
                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
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
        public async Task GetDeviceEntryList()
        {
            CT.ThrowIfCancellationRequested();
            DeviceEntriesList = new List<DeviceEntry> { };
            DeviceErrorList = new List<DBEntry> { };
            foreach (DataRow row in AccessDBEntries)
            {
                string devName = Regex.Replace(row.Field<string>(DBP.Vehicle), @"\s\d*", "");
                if (string.IsNullOrEmpty(devName)) continue;
                var dbEntry = new DBEntry(new DBEntryParams
                {
                    Id = row.Field<int>(DBP.Id),
                    Vehicle = devName,
                    Timestamp = row.Field<DateTime>(DBP.Time)
                });
                if (_deviceNameCache.TryGetValue(devName, out Id devId))
                    DeviceEntriesList.Add(new DeviceEntry(dbEntry, _deviceCache[devId]));
                else
                    DeviceErrorList.Add(dbEntry);
            }
            await UpdateProgress(45);
        }
        public async Task GetCallsList(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            odometerCalls = new List<object> { };
            engineHoursCalls = new List<object> { };
            locationCalls = new List<object> { };
            foreach (DeviceEntry deviceEntry in DeviceEntriesList)
            {
                Device device = deviceEntry.Device;
                DBEntry entry = deviceEntry.Entry;
                odometerCalls.Add(new object[] {
                    "Get", typeof(StatusData), new { search = new StatusDataSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DiagnosticSearch = new DiagnosticSearch(KnownId.DiagnosticOdometerAdjustmentId),
                        DeviceSearch = new DeviceSearch(device.Id)
                    } }, ct, typeof(List<StatusData>)});
                engineHoursCalls.Add(new object[] {
                    "Get", typeof(StatusData), new { search = new StatusDataSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DiagnosticSearch = new DiagnosticSearch(KnownId.DiagnosticEngineHoursAdjustmentId),
                        DeviceSearch = new DeviceSearch(device.Id)
                    } }, ct, typeof(List<StatusData>)});
                locationCalls.Add(new object[] {"Get", typeof(LogRecord), new { search = new LogRecordSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DeviceSearch = new DeviceSearch
                        {
                            Id = device.Id
                        }
                    } }, ct, typeof(List<LogRecord>)});
            }
            await UpdateProgress(50);
            return;
        }
        public async Task GetApiResults(CancellationToken ct)
        {
            List<object>[] results;
            var oCall = Program.API.MultiCallAsync(odometerCalls.ToArray());
            var eHoursCall = Program.API.MultiCallAsync(engineHoursCalls.ToArray());
            var lCall = Program.API.MultiCallAsync(locationCalls.ToArray());
            try
            {
                results = await Task.WhenAll(oCall, eHoursCall, lCall);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            CT.ThrowIfCancellationRequested();
            var oCallResults = results[0];
            var eHoursCallResults = results[1];
            var lCallResults = results[2];
            await UpdateProgress(75);

            GeoGoEntries = new List<GeoGo_Entry> { };
            for (int i = 0; i < oCallResults.Count; i++)
            {
                StatusData odometer = ((List<StatusData>)oCallResults[i])[0];
                StatusData engineHours = ((List<StatusData>)eHoursCallResults[i])[0];
                LogRecord location = ((List<LogRecord>)lCallResults[i])[0];
                Device device = DeviceEntriesList[i].Device;
                DBEntry entry = DeviceEntriesList[i].Entry;
                GeoGo_EntryData GeoGoData = new GeoGo_EntryData
                {
                    Device = device,
                    DeviceEngineHours = engineHours,
                    DeviceMileage = odometer,
                    DeviceLocation = location,
                    Entry = entry
                };
                GeoGoEntries.Add(new GeoGo_Entry(GeoGoData));
            }
            await UpdateProgress(90);
            return;
        }
        private async Task GetGeoGoTable()
        {
            CT.ThrowIfCancellationRequested();
            DataTable dt = new DataTable();
            dt.Columns.Add("Entry ID");
            dt.Columns.Add("Time");
            dt.Columns.Add("Deivce Name");
            dt.Columns.Add("Odometer");
            dt.Columns.Add("Engine Hours");
            dt.Columns.Add("Location");
            foreach (GeoGo_Entry GeoGo in GeoGoEntries)
            {
                DataRow dr = dt.NewRow();
                dr[0] = GeoGo.EntryID;
                dr[1] = GeoGo.Timestamp;
                dr[2] = GeoGo.DeviceName;
                dr[3] = GeoGo.Miles;
                dr[4] = GeoGo.Hours;
                dr[5] = GeoGo.Location;
                dt.Rows.Add(dr);
            }
            foreach (DBEntry dbe in DeviceErrorList)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dbe.Id;
                dr[1] = dbe.Timestamp;
                dr[2] = dbe.Vehicle;
                dr[3] = -3;
                dr[4] = 0;
                dr[5] = 0;
                dt.Rows.Add(dr);
            }
            GeoGoTable = dt;
            await UpdateProgress(100, "Done");
            return;
        }
        public void UpdateAccessDB()
        {
            DataTable dt = GeoGoTable;
            OleDbConnection con = new OleDbConnection(ConStr);
            using (con)
            {                
                int updated = 0;
                try
                {
                    con.Open();
                    OleDbTransaction transaction = con.BeginTransaction();
                    DataRowCollection drs = dt.Rows;
                    foreach (DataRow dr in drs)
                    {
                        var oReading = dr.Field<string>("Odometer");
                        var entryId = dr.Field<string>("Entry ID");
                        string query = $"UPDATE [{DBP.Table}] SET [{DBP.Odometer}] = '{oReading}' WHERE [{DBP.Id}] = {entryId};";
                        using (OleDbCommand cmd = new OleDbCommand(query, con, transaction))
                        {
                            updated += cmd.ExecuteNonQuery();
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
                    if (!GGP.IsDisposed)
                        GGP.InsertBtn.Enabled = false;
                    MessageBox.Show($"{updated} out of {dt.Rows.Count} entries updated successfully.", "DB Update Complete");
                }
            }
        }
    }
}
