﻿using Access_GeoGo.Data.Geotab;
using Geotab.Checkmate.ObjectModel;
using Geotab.Checkmate.ObjectModel.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Access_GeoGo.Forms
{
    public partial class FaultCodesPage : Form
    {
        private static Dictionary<Id, Device> _deviceCache;
        private static Dictionary<string, Id> _deviceNameCache;
        private static Dictionary<Id, Controller> _controllerCache;
        private static Dictionary<Id, Diagnostic> _diagnosticCache;
        private static Dictionary<Id, FailureMode> _fmiCache;
        private readonly GeotabApi _geotabApi;

        public FaultCodesPage()
        {
            InitializeComponent();
            CancellationTokenSource cts = new CancellationTokenSource();
            _geotabApi = new GeotabApi(cts.Token);
        }

        private async void ResultsBtn_Click(object sender, EventArgs e)
        {
            List<FaultData> faultData = await GetFaultCodes();
            DisplayCodes(faultData);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private async void GetFeedBtn_Click(object sender, EventArgs e)
        {
            FeedResult<FaultData> faultFeed = await GetFaultFeed();
            DisplayCodes(faultFeed);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private async void FaultCodesPage_Load(object sender, EventArgs e)
        {
            await Task.WhenAll(GetDeviceList(), GetControllerCache(), GetDiagnosticCache(), GetFmiCache());
            GetFeedBtn.Enabled = true;
            SearchBtn.Enabled = true;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /*
        private async Task GetCachedData()
        {
            Task<Dictionary<Id, Controller>> controller = _geotabApi.GetDictionary<Controller, Id>(c => c.Id);
            Task<Dictionary<Id, Diagnostic>> diagnostic = _geotabApi.GetDictionary<Diagnostic, Id>(c => c.Id);
            Task<Dictionary<Id, FailureMode>> fmi = _geotabApi.GetDictionary<FailureMode, Id>(f => f.Id);
            await Task.WhenAll(controller, diagnostic, fmi);
            _diagnosticCache = diagnostic.Result;
            _controllerCache = controller.Result;
            _fmiCache = fmi.Result;
        }*/

        private async Task GetDeviceList()
        {
            await GetDeviceCache();
            DeviceComboBox.Items.AddRange(_deviceNameCache.Keys.ToArray<object>());
        }
        private async Task GetDeviceCache()
        {
            if (_deviceCache == null) _deviceCache = await _geotabApi.GetDictionary<Device, Id>(d => d.Id);
            if (_deviceNameCache == null) _deviceNameCache = _deviceCache.ToDictionary(d => d.Value.Name, d => d.Key);
        }
        private async Task GetControllerCache()
        {
            if (_controllerCache == null) _controllerCache = await _geotabApi.GetDictionary<Controller, Id>(c => c.Id);
        }
        private async Task GetDiagnosticCache()
        {
            if (_diagnosticCache == null) _diagnosticCache = await _geotabApi.GetDictionary<Diagnostic, Id>(c => c.Id);
        }
        private async Task GetFmiCache()
        {
            if (_fmiCache == null) _fmiCache = await _geotabApi.GetDictionary<FailureMode, Id>(f => f.Id);
        }

        private async Task<FeedResult<FaultData>> GetFaultFeed()
        {
            long faultData = Convert.ToInt64(Program.Config.GeotabFeeds.Users[Program.Config.UserConfig.Name].DataFeeds["FaultData"].Token);
            FeedResult<FaultData> feedResults = await _geotabApi.Get<FeedResult<FaultData>, FaultData>("GetFeed", new
            {
                resultsLimit = 10000,
                fromVersion = faultData
            });
            Program.Config.GeotabFeeds.Users[Program.Config.UserConfig.Name].DataFeeds["FaultData"].Token = feedResults.ToVersion.ToString();
            Program.Config.AppConfig.Save();
            return feedResults;
        }

        private async Task<List<FaultData>> GetFaultCodes()
        {
            FaultDataSearch faultDataSearch = new FaultDataSearch();
            if (FromDatePicker.Checked) faultDataSearch.FromDate = FromDatePicker.Value;
            if (ToDatePicker.Checked) faultDataSearch.ToDate = ToDatePicker.Value;
            if (!string.IsNullOrEmpty(DeviceComboBox.Text))
                faultDataSearch.DeviceSearch = new DeviceSearch
                {
                    Id = _deviceNameCache[DeviceComboBox.Text]
                };
            return await _geotabApi.Get<List<FaultData>, FaultData>("Get", new
            {
                resultsLimit = LimitSelection.Value,
                search = faultDataSearch
            });
        }

        private void FaultCodeView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ClientSize = new Size(Math.Max(FaultCodeView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 20, ClientSize.Width), ClientSize.Height);
            MinimumSize = SizeFromClientSize(ClientSize);
        }

        private void DisplayCodes(List<FaultData> faultData)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Deivce Name");
            dt.Columns.Add("Code");
            dt.Columns.Add("Description");
            dt.Columns.Add("Controller");
            dt.Columns.Add("FMI");
            dt.Columns.Add("Source");
            dt.Columns.Add("Date");
            foreach (FaultData code in faultData)
            {
                DataRow dr = dt.NewRow();
                dr = FaultCodeRow(dr, code);
                dt.Rows.Add(dr);
            }
            FaultCodeView.DataSource = dt;
        }

        private void DisplayCodes(FeedResult<FaultData> faultData)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Deivce Name");
            dt.Columns.Add("Code");
            dt.Columns.Add("Description");
            dt.Columns.Add("Controller");
            dt.Columns.Add("FMI");
            dt.Columns.Add("Source");
            dt.Columns.Add("Date");
            MessageBox.Show($"{faultData.Data.Count}");
            foreach (FaultData code in faultData.Data)
            {
                DataRow dr = dt.NewRow();
                dr = FaultCodeRow(dr, code);
                dt.Rows.Add(dr);
            }
            FaultCodeView.DataSource = dt;
        }

        private DataRow FaultCodeRow(DataRow dr, FaultData fd)
        {
            fd.Diagnostic = _diagnosticCache[fd.Diagnostic.Id];
            fd.Device = _deviceCache[fd.Device.Id];
            fd.Controller = _controllerCache[fd.Controller.Id];
            fd.FailureMode = _fmiCache[fd.FailureMode.Id];
            dr[0] = fd.Device.Name;
            dr[1] = (fd.Diagnostic.Code ?? 0).ToString("X");
            dr[2] = fd.Diagnostic.Name;
            dr[3] = fd.Controller.Name;
            dr[4] = fd.FailureMode.Name;
            dr[5] = fd.Diagnostic.Source.Name;
            dr[6] = fd.DateTime;
            return dr;
        }
    }

    /*
    public class GeoGoCache<TItem>
    {
        private Microsoft.Extensions.Caching.Memory.MemoryCache _cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions()
        {
            SizeLimit = 1024
        });
        public async Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem)
        {
            TItem cacheEntry;
            if (!_cache.TryGetValue(key, out cacheEntry))// Look for cache key.
            {
                // Key not in cache, so get data.
                cacheEntry = await createItem();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                 .SetSize(1)//Size amount
                            //Priority on removing when reaching size limit (memory pressure)
                    .SetPriority(Microsoft.Extensions.Caching.Memory.CacheItemPriority.High);
                // Keep in cache for this time, reset time if accessed.
                //.SetSlidingExpiration(TimeSpan.FromSeconds(2));

                // Save data in cache.
                _cache.Set(key, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }
    }
    public class GeoGoDeviceCache
    {
        private Microsoft.Extensions.Caching.Memory.MemoryCache _cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions()
        {
            SizeLimit = 1024
        });
        public async Task<Device> GetDevice(object key)
        {
            if (!_cache.TryGetValue(key, out List<Device> cacheEntry))// Look for cache key.
            {
                MessageBox.Show($"{key} was not in cache");
                // Key not in cache, so get data.
                cacheEntry = await Program.API.CallAsync<List<Device>>("Get", typeof(Device), new { resultsLimit = 1, search = new DeviceSearch { Id = key as Id } });

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(1)//Size amount
                               //Priority on removing when reaching size limit (memory pressure)
                    .SetPriority(Microsoft.Extensions.Caching.Memory.CacheItemPriority.High);
                // Keep in cache for this time, reset time if accessed.
                //.SetSlidingExpiration(TimeSpan.FromSeconds(2));

                // Save data in cache.
                _cache.Set(key, cacheEntry[0], cacheEntryOptions);
            }
            return cacheEntry[0];
        }
        public async Task SetDeviceCache()
        {
            List<Device> cacheList = await Program.API.CallAsync<List<Device>>("Get", typeof(Device));
            foreach (Device cacheEntry in cacheList)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(1)//Size amount
                               //Priority on removing when reaching size limit (memory pressure)
                    .SetPriority(Microsoft.Extensions.Caching.Memory.CacheItemPriority.High);
                // Keep in cache for this time, reset time if accessed.
                //.SetSlidingExpiration(TimeSpan.FromSeconds(2));

                // Save data in cache.
                _cache.Set(cacheEntry.Id, cacheEntry, cacheEntryOptions);
            }
        }
        static void GiveMeTheDate(Action<int, string> action)
        {
            var now = DateTime.Now;
            action(now.Day, now.ToString("MMMM"));
        }

        //GiveMeTheDate((day, month) => Console.WriteLine("Day: {0}, Month: {1}", day, month));
        public void ForEach(Action<Device> action)
        {
            action();
        }
    }

    public static class Caching
    {
        /// <summary>
        /// A generic method for getting and setting objects to the memory cache.
        /// </summary>
        /// <typeparam name="T">The type of the object to be returned.</typeparam>
        /// <param name="cacheItemName">The name to be used when storing this object in the cache.</param>
        /// <param name="cacheTimeInMinutes">How long to cache this object for.</param>
        /// <param name="objectSettingFunction">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        public static T GetObjectFromCache<T>(string cacheItemName, int cacheTimeInMinutes, Func<T> objectSettingFunction)
        {
            ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;
            var cachedObject = (T)cache[cacheItemName];
            if (cachedObject == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes);
                cachedObject = objectSettingFunction();
                cache.Set(cacheItemName, cachedObject, policy);
            }
            return cachedObject;
        }
    }*/
}

/*
                DataRow dr = dt.NewRow();
                dr[0] = code.Device.Id;
                dr[1] = code.Controller.Name;
                dr[2] = code.Diagnostic.Name;
                dr[3] = code.Diagnostic.Code;
                dr[4] = code.FailureMode.Name;
                dr[5] = code.Diagnostic.Source;
                dt.Rows.Add(dr);*/

/* api.call("Get", {
    typeName : "FaultData",
    search: {
        deviceSearch: {
            id: "b33"
        },
        fromDate: fromDate
    },
    resultsLimit: 100
}, function (devices) {
    console.log(devices);
    GetData(devices);
});

function GetData(dev) {
    dev.forEach(function (d) {
        var device, date, description, fmi, fmiId, source, controller, code;
        api.call("Get", {
            typeName : "Diagnostic",
            search: {
                id: d.diagnostic.id
            },
            resultsLimit: 1
        }, function (di) {
            source = di[0].source;
            date = new Date(d.dateTime).toString();
            description = di[0].name;
            code = di[0].code;
            var calls = [
                ["Get", {
                        typeName : "Device",
                        search: {
                            id : d.device.id
                        },
                        resultsLimit: 1}],
                ["Get", {
                        typeName : "Controller",
                        search: {
                            id: d.controller.id
                        },
                        resultsLimit: 1}]
            ];
            if (d.failureMode.id != null) {
                calls.push(["Get", {
                        typeName : "FailureMode",
                        search: {
                            id: d.failureMode.id
                        },
                        resultsLimit: 1}]);
            }

            api.multiCall(calls, function (results) {
                results.push(d);
                console.log(results);
                var devObj = results[0];
                var contrObj = results[1];
                var fmiObj = results[2];
                device = devObj[0].name;
                if (fmiObj[0] != undefined) {
                    fmi = fmiObj[0].name;
                    fmiId = fmiObj[0].code;
                } else {
                    console.log("<<<<<<<<<<<<No FMI>>>>>>>>>>>")
                    fmi = "Not Given";
                    fmiId = "-";
                }

                controller = contrObj[0].name;

                console.log(`${device} | ${date}
                | ${description} | ${fmi}
                | ${fmiId} | ${source} | ${controller}
                | ${code}`);
            }, function (err) {
            console.log("Error: " + err);
        });
        }, function (err) {
            console.log("Error:" + err);
        });
    })
}
*/