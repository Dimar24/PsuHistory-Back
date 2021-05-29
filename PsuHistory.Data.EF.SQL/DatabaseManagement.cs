using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PsuHistory.Data.EF.SQL.Context;

namespace PsuHistory.Data.EF.SQL
{
    public static class DatabaseManagement
    {
        // Getting the scope of our database context
        public static void MigrationInitialisation(IApplicationBuilder app, string typeDatabase)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                switch (typeDatabase)
                {
                    case "MsSql": serviceScope.ServiceProvider.GetService<DbContextMsSql>().Database.Migrate(); break;
                    case "MySql": serviceScope.ServiceProvider.GetService<DbContextMySql>().Database.Migrate(); break;
                    case "Postgres": serviceScope.ServiceProvider.GetService<DbContextPostgres>().Database.Migrate(); break;
                }
            }
        }
    }
}
