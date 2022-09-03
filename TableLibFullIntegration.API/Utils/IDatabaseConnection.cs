using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TableLibFullIntegration.API.Utils
{
    public interface IDatabaseConnection
    {
        public Task<T> GetSingleAsync<T>(string query, object parameter = null);
        public Task<IEnumerable<T>> GetAllAsync<T>(string query, object parameter = null);
        public Task<int> ExecuteAsync(string query, object parameter = null);
    }
}