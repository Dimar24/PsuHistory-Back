using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PsuHistory.Data.EF.SQL.Context;
using System;

namespace PsuHistory.Data.EF.SQL.ContextFactory
{
    public class DbContextFactoryPostgres : DbContextFactoryBase, IDesignTimeDbContextFactory<DbContextPostgres>
    {
        public DbContextPostgres CreateDbContext(string[] args)
        {
            optionsBuilder = new DbContextOptionsBuilder<DbContextPostgres>();

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseNpgsql(GetConnectionString("PostgresConnection"));

            return new DbContextPostgres(optionsBuilder.Options);
        }
    }
}
