using Dapper;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace CompanyEmployees.Migrations
{
    public class Database
    {
        private readonly DapperContext _dapperContext;

        public Database(DapperContext dapperContext)=> _dapperContext = dapperContext;

        public void CreateDatabase(string dbName)
        {
            var query = "SELECT * FROM sys.databases WHERE name = @name";

            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);

            using (var connection = _dapperContext.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);

                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }
        }
    }
}
