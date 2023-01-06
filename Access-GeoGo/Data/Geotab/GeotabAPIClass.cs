using Geotab.Checkmate;
using Geotab.Checkmate.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Access_GeoGo.Data.Geotab
{
    internal class GeotabApi : IDisposable
    {
        private static API _api = Program.Api;
        private static CancellationToken _ct;
        private bool _disposed;

        public GeotabApi(CancellationToken ct) => _ct = ct;

        public void UpdateCt(CancellationToken ct) => _ct = ct;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) { }
            _api = null;
            _disposed = true;
        }

        //TODO: Add Code Documentation
        public async Task<TResult> Get<TResult, TType>(string getMethod, object @params = null)
        {
            TResult @return = default;
            try
            {
                @return = await _api.CallAsync<TResult>(getMethod, typeof(TType), @params, _ct);
            }
            catch (OperationCanceledException) { }
            return @return;
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
            Dictionary<TKey, TSource> sDict = null;
            try
            {
                List<TSource> sList = await _api.CallAsync<List<TSource>>("Get", typeof(TSource), null, _ct);
                sDict = sList.ToDictionary(getKey);
            }
            catch (OperationCanceledException) { }
            return sDict;
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
            Dictionary<TKey, TValue> sDict = null;
            try
            {
                List<TSource> sList = await _api.CallAsync<List<TSource>>("Get", typeof(TSource), null, _ct);
                sDict = sList.ToDictionary(getKey, getValue);
            }
            catch (OperationCanceledException) { }
            return sDict;
        }

        public class MultiCallList<TType>
        {
            private readonly List<object> _callsList;
            private List<object> _resultsList;

            public MultiCallList() => _callsList = new List<object>();

            public void AddCall(string getMethod, object @params = null)
            {
                _callsList.Add(new[] { getMethod, typeof(TType), @params, _ct, typeof(List<TType>) });
            }

            public async Task<List<TType>> ExecuteGetResults()
            {
                await Execute();
                return GetResults();
            }

            public List<TType> GetResults()
            {
                List<TType> resultList = new List<TType>();
                if (!(_resultsList is null))
                    resultList.AddRange(from List<TType> results in _resultsList
                        select results.Count >= 1 ? results[0] : default);
                return resultList;
            }

            public async Task<MultiCallList<TType>> Execute()
            {
                try
                {
                    _resultsList = await _api.MultiCallAsync(_callsList.ToArray());
                }
                catch (OperationCanceledException) { }
                return this;
            }
        }
    }
}