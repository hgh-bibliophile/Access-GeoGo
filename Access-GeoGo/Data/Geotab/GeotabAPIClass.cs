using Geotab.Checkmate;
using Geotab.Checkmate.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Access_GeoGo.Data.Geotab
{
    internal class GeotabAPI : IDisposable
    {
        private static API API;
        private static CancellationToken CT;
        private bool disposed;

        public GeotabAPI(API api, CancellationToken ct)
        {
            API = api;
            CT = ct;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing) { }
                API = null;
                disposed = true;
            }
        }

        /// <summary>
        /// Gets a dictionary of all of the items of a specified GeoTab object class.
        /// </summary>
        /// <typeparam name="TSource">The Geotab object class to search for. (e.g. <see cref="Device"/>)</typeparam>
        /// <typeparam name="TKey">The typename of the dictionary key. (e.g. <see cref="Id"/>)</typeparam>
        /// <param name="getKey">A function to extract the key from each element. (e.g. Return the <see cref="Device.Id"/>)</param>
        /// <returns><see cref="Dictionary{TKey, TValue}"/></returns>
        public async Task<Dictionary<TKey, TSource>> GetDictionary<TSource, TKey>(Func<TSource, TKey> getKey)
        {
            Dictionary<TKey, TSource> TSourceDictionary = null;
            try
            {
                var TSourceList = await API.CallAsync<List<TSource>>("Get", typeof(TSource), null, CT);
                TSourceDictionary = TSourceList.ToDictionary(s => getKey(s));
            }
            catch (OperationCanceledException) { }
            return TSourceDictionary;
        }

        /// <summary>
        /// Gets a dictionary of all of the items of a specified GeoTab object class.
        /// </summary>
        /// <typeparam name="TSource">The Geotab object class to search for. (e.g. <see cref="Device"/>)</typeparam>
        /// <typeparam name="TKey">The typename of the dictionary key. (e.g. <see cref="Id"/>)</typeparam>
        /// <typeparam name="TValue">The typename of the dictionary value. (e.g. The <see cref="Device"/> name)</typeparam>
        /// <param name="getKey">A function to extract the key from each element. (e.g. Return the <see cref="Device.Id"/>)</param>
        /// <param name="getValue">A transform function to produce a result element value from each element. (e.g. Return the <see cref="Device.Name"/>)</param>
        /// <returns><see cref="Dictionary{TKey, TValue}"/></returns>
        public async Task<Dictionary<TKey, TValue>> GetDictionary<TSource, TKey, TValue>(Func<TSource, TKey> getKey, Func<TSource, TValue> getValue)
        {
            Dictionary<TKey, TValue> TSourceDictionary = null;
            try
            {
                var TSourceList = await API.CallAsync<List<TSource>>("Get", typeof(TSource), null, CT);
                TSourceDictionary = TSourceList.ToDictionary(s => getKey(s), s => getValue(s));
            }
            catch (OperationCanceledException) { }
            return TSourceDictionary;
        }

        //TODO: Add Code Documentation
        public async Task<TResult> Get<TResult, TType>(string GetMethod, object Params = null)
        {
            TResult TReturn = default;
            try
            {
                TReturn = await API.CallAsync<TResult>(GetMethod, typeof(TType), Params, CT);
            }
            catch (OperationCanceledException) { }
            return TReturn;
        }

        public class MultiCallList<TType>
        {
            private readonly List<object> CallsList;
            private List<object> ResultsList;

            public MultiCallList() => CallsList = new List<object>();

            public void AddCall(string GetMethod, object Params = null)
            {
                CallsList.Add(new object[] { GetMethod, typeof(TType), Params, CT, typeof(List<TType>) });
            }

            public async Task<List<TType>> GetCallResults()
            {
                List<TType> TResultList = null;
                try
                {
                    var TGetResults = await API.MultiCallAsync(CallsList.ToArray());
                    TResultList = (from List<TType> TResults in TGetResults
                                   select TResults[0]).ToList();
                }
                catch (OperationCanceledException) { }
                return TResultList;
            }

            public async Task<MultiCallList<TType>> MakeCall()
            {
                try
                {
                    ResultsList = await API.MultiCallAsync(CallsList.ToArray());
                }
                catch (OperationCanceledException) { }
                return this;
            }

            public List<TType> GetResults()
            {
                List<TType> TResultList = new List<TType>();
                //x List<Device> dt = new List<Device>();
                //x dt.Add(null);
                if (!(ResultsList is null))
                    foreach (List<TType> TResults in ResultsList)
                    {
                        if (TResults.Count >= 1)
                            TResultList.Add(TResults[0]);
                        else
                            TResultList.Add(default);
                    }
                return TResultList;
            }
        }
    }
}