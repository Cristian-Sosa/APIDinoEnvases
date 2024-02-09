using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace DinoEmpleoAPI.Data
{
    public class DatabaseSingleton
    {
        private readonly string _connectionString;
        private static readonly DatabaseSingleton _instance = new();

        public static DatabaseSingleton Instance
        {
            get { return _instance; }
        }

        private DatabaseSingleton()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            _connectionString = builder.GetSection("ConnectionStrings:SQLServer").Value;
        }


        public IEnumerable<T> ExecuteQuery<T>(string query, object? parameters)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            IEnumerable<T> data = connection.Query<T>(query, parameters);
            connection.Close();
            return data;
        }

        public async Task<bool> ExecuteQueryTransacction(string query, object? parameters)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            bool data = false;
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                await connection.ExecuteAsync(query, parameters, transaction);
                transaction.Commit();
                data = true;
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
            }
            return data;
        }
    }
}
