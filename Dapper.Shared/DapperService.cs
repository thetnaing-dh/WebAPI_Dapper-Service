using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Dapper.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;
        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<T> Query<T>(string query, object? param = null) 
        { 
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<T>(query, param).ToList();            
        }

        public T QueryFirstOrDefault<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.QueryFirstOrDefault<T>(query, param);
            return item;
        }

        public int Execute(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);   
            int result = db.Execute(query, param);
            return result;
        }
    }
}