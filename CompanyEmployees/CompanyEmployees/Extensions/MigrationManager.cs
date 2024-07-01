using CompanyEmployees.Migrations;
using Contracts;

namespace CompanyEmployees.Extensions
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using (var scope= app.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider
                    .GetRequiredService<Database>();

                try
                {
                    databaseService.CreateDatabase("CompanyEmployeeDapper");
                }
                catch (Exception ex)
                {

                    var logger = app.Services.GetRequiredService<ILoggerManager>();
                    logger.LogError($"Exception occurred during the database creation: { ex}");
                   throw;
                }
            }

            return app;
        }
    }
}
