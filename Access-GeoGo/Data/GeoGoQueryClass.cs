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
using Access_GeoGo.Data.Geotab;

namespace Access_GeoGo.Data
{
    class GeoGoQuery : IDisposable
    {
        private FuelTransPage GGP;
        private DBParamsPage DBP;
        private string ConStr;
        private CancellationToken CT;
        private DataTable GeoGoTable = null;
        static GeotabAPI GeotabAPI;
        //Private Variables
        #region Results Calls & Lists; Device & GeoGo Entry Lists
        private DataRow[] AccessDBEntries;
        private GeotabAPI.MultiCallList<StatusData> odometerCalls;
        private GeotabAPI.MultiCallList<StatusData> engineHoursCalls;
        private GeotabAPI.MultiCallList<LogRecord> locationCalls;
        private GeotabAPI.MultiCallList<DriverChange> driverCalls;

        static Dictionary<Id, Device> _deviceCache;
        static Dictionary<string, Id> _deviceNameCache;
        static Dictionary<Id, User> _driverCache;
        static Dictionary<string, Id> _driverNameCache;

        static List<DeviceEntry> DeviceEntriesList;
        static List<DBEntry> DeviceErrorList;
        static List<GeoGo_Entry> GeoGoEntries;
        public static bool IsComplete = false;
        private int RecordsCt;
        #endregion
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
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
                    GeotabAPI.Dispose();
                    // Dispose managed resources.
                    //component.Dispose();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
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
                _driverNameCache = null;
                DeviceEntriesList = null;
                DeviceErrorList = null;
                GeoGoEntries = null;
                // Note disposing has been done.
                disposed = true;
            }
        }
        public GeoGoQuery(FuelTransPage GeoGoPage, CancellationToken ct)
        {
            GGP = GeoGoPage;
            DBP = GeoGoPage.DBP;
            CT = ct;
            GeotabAPI = new GeotabAPI(Program.API, ct);
            ConStr = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + DBP.File + "; Persist Security Info = False";
        }

        public async Task GeoGoQueryAsync()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                var prep = PrepUIPage();
                var devCache = GetDeviceCache();
                var drvrCache = GetDriverCache();
                var getDB = GetDBEntries();
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
        public async Task GetDeviceCache()
        {
            _deviceCache = await GeotabAPI.GetDictionary<Device, Id>(d => d.Id);
            _deviceNameCache = _deviceCache.ToDictionary(d => d.Value.Name, d => d.Key);
            await UpdateProgress(40);
        }
        public async Task GetDriverCache()
        {
            _driverCache = await GeotabAPI.GetDictionary<User, Id>(d => d.Id);
            _driverNameCache = _driverCache.ToDictionary(d => d.Value.Name, d => d.Key);
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
            var id = DBP.Id;
            var time = DBP.Time;
            foreach (DataRow row in AccessDBEntries)
            {
                string devName = Regex.Replace(row.Field<string>(DBP.Vehicle), @"\s\d*", "");
                if (string.IsNullOrEmpty(devName)) continue;
                var dbEntry = new DBEntry(new DBEntryParams
                {
                    Id = row.Field<int>(id),
                    Vehicle = devName,
                    Timestamp = row.Field<DateTime>(time)
                });
                if (_deviceNameCache.TryGetValue(devName, out Id devId))
                    DeviceEntriesList.Add(new DeviceEntry(dbEntry, _deviceCache[devId]));
                else
                    DeviceErrorList.Add(dbEntry);
            }
            await UpdateProgress(45);
        }
        public async Task GetCallsList()
        {
            if (CT.IsCancellationRequested) return;
            odometerCalls = new GeotabAPI.MultiCallList<StatusData>();
            engineHoursCalls = new GeotabAPI.MultiCallList<StatusData>();
            locationCalls = new GeotabAPI.MultiCallList<LogRecord>();
            driverCalls = new GeotabAPI.MultiCallList<DriverChange>();
            foreach (DeviceEntry deviceEntry in DeviceEntriesList)
            {
                Device device = deviceEntry.Device;
                DBEntry entry = deviceEntry.Entry;
                odometerCalls.AddCall("Get", new { search = new StatusDataSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DiagnosticSearch = new DiagnosticSearch(KnownId.DiagnosticOdometerAdjustmentId),
                        DeviceSearch = new DeviceSearch(device.Id)
                    } });
                engineHoursCalls.AddCall("Get", new { search = new StatusDataSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DiagnosticSearch = new DiagnosticSearch(KnownId.DiagnosticEngineHoursAdjustmentId),
                        DeviceSearch = new DeviceSearch(device.Id)
                    } });
                locationCalls.AddCall("Get", new { search = new LogRecordSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DeviceSearch = new DeviceSearch
                        {
                            Id = device.Id
                        }
                    } });
                driverCalls.AddCall("Get", new { search = new DriverChangeSearch
                    {
                        FromDate = entry.Timestamp,
                        ToDate = entry.Timestamp,
                        DeviceSearch = new DeviceSearch
                        {
                            Id = device.Id
                        },
                        IncludeOverlappedChanges = true
                    } });

            }
            await UpdateProgress(50);
            return;
        }
        public async Task GetApiResults()
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
            var oCallResults = odometerCalls.GetResults();
            var eHoursCallResults = engineHoursCalls.GetResults();
            var lCallResults = locationCalls.GetResults();
            var dCallResults = driverCalls.GetResults();
            await UpdateProgress(75);

            GeoGoEntries = new List<GeoGo_Entry> { };
            for (int i = 0; i < oCallResults.Count; i++)
            {
                StatusData odometer = oCallResults[i];
                StatusData engineHours = eHoursCallResults[i];
                LogRecord location = lCallResults[i];
                string driver = (dCallResults[i] != null) && _driverCache.TryGetValue(dCallResults[i].GetDriverId(), out User user) ? user.Name : "Unknown Driver";
                Device device = DeviceEntriesList[i].Device;
                DBEntry entry = DeviceEntriesList[i].Entry;
                GeoGo_EntryData GeoGoData = new GeoGo_EntryData
                {
                    Device = device,
                    DeviceEngineHours = engineHours,
                    DeviceMileage = odometer,
                    DeviceLocation = location,
                    Driver = driver,
                    Entry = entry
                };
                GeoGoEntries.Add(new GeoGo_Entry(GeoGoData));
            }
            await UpdateProgress(90);
            return;
        }
        private async Task GetGeoGoTable()
        {
            if (CT.IsCancellationRequested) return;
            DataTable dt = new DataTable();
            dt.Columns.Add("Entry ID");
            dt.Columns.Add("Time");
            dt.Columns.Add("Deivce Name");
            dt.Columns.Add("Odometer");
            dt.Columns.Add("Engine Hours");
            dt.Columns.Add("Location");
            dt.Columns.Add("Driver");
            foreach (GeoGo_Entry GeoGo in GeoGoEntries)
            {
                DataRow dr = dt.NewRow();
                dr[0] = GeoGo.EntryID;
                dr[1] = GeoGo.Timestamp;
                dr[2] = GeoGo.DeviceName;
                dr[3] = GeoGo.Miles;
                dr[4] = GeoGo.Hours;
                dr[5] = GeoGo.Location;
                dr[6] = GeoGo.Driver;
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
                dr[6] = "";
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
/*
public static class DictionaryExtensionsClass
{
    public static Dictionary<TKey, TElement> ToSafeDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
    {
        if (source == null)
            throw new ArgumentException("source");
        if (keySelector == null)
            throw new ArgumentException("keySelector");
        if (elementSelector == null)
            throw new ArgumentException("elementSelector");
        Dictionary<TKey, TElement> d = new Dictionary<TKey, TElement>(comparer);
        foreach (TSource element in source)
        {
            if (!d.ContainsKey(keySelector(element)))
                d.Add(keySelector(element), elementSelector(element));
        }

        return d;
    }
}*/

/*          var ListOFValues = _deviceCache.Select(kv => kv.Value.Name).ToList();
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            //count them all:
            ListOFValues.ForEach(word =>
            {
                string key = word.ToLower();
                if (!wordCount.ContainsKey(key))
                    wordCount.Add(key, 0);
                wordCount[key]++;
            });

            //remove words appearing only once:
            wordCount.Keys.ToList().FindAll(word => wordCount[word] == 1).ForEach(key => wordCount.Remove(key));

            Console.WriteLine(string.Format("Found {0} duplicates in the list:", wordCount.Count));
            wordCount.Keys.ToList().ForEach(key => Console.WriteLine(string.Format("{0} appears {1} times", key, wordCount[key])));
*/
