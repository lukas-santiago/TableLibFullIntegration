using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;

namespace TableLibFullIntegration.API.Utils
{
    public class MySQLDatabaseConnection : IDatabaseConnection
    {
        private readonly IConnectionConfiguration ConnectionConfiguration;
        public MySQLDatabaseConnection(IConnectionConfiguration connectionConfiguration)
        {
            this.ConnectionConfiguration = connectionConfiguration;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionConfiguration.ConnectionString);
        }
        public async Task<T> GetSingleAsync<T>(string query, object parameter = null)
        {
            using var connection = GetConnection();
            connection.Open();
            var result = await connection.QuerySingleOrDefaultAsync<T>(query, parameter);
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string query, object parameter = null)
        {
            using var connection = GetConnection();
            connection.Open();
            var result = await connection.QueryAsync<T>(query, parameter);
            return result;
        }

        public async Task<int> ExecuteAsync(string query, object parameter = null)
        {
            using var connection = GetConnection();
            connection.Open();
            var result = await connection.ExecuteAsync(query, parameter);
            return result;
        }
    }
}