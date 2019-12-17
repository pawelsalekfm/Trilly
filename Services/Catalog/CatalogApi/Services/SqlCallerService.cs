using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CatalogApi.Services
{
    public static class SqlCallerService
    {
        public static async Task<TResponse> CallDatabase <TResponse>(string connectionString, string sqlCommand,
            Func<SqlConnection, Task<TResponse>> func)
        {
            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                return await func(connection);
                //return await connection.QueryAsync<TResponse>(sqlCommand);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }
    }
}
