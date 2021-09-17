using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SubscribeAsyncExcerise
{
    internal class SearchEngine
    {
        #region Fields

        private readonly string _searchEngineName;

        #endregion

        #region Constructor

        public SearchEngine(string searchEngineName)
        {
            if (string.IsNullOrWhiteSpace(searchEngineName))
            {
                throw new ArgumentException($"'{nameof(searchEngineName)}' cannot be null or whitespace.", nameof(searchEngineName));
            }

            _searchEngineName = searchEngineName;
        }

        #endregion

        #region Methods

        public async Task<List<string>> SearchAsync(string query, CancellationToken cancellationToken)
        {
            List<string> results = new();

            await Task.Delay(2000);

            cancellationToken.ThrowIfCancellationRequested();

            results.Add($"{_searchEngineName} ({query}): {Guid.NewGuid()}");

            await Task.Delay(2000);

            cancellationToken.ThrowIfCancellationRequested();

            results.Add($"{_searchEngineName} ({query}): {Guid.NewGuid()}");

            return results;
        }

        #endregion
    }
}
