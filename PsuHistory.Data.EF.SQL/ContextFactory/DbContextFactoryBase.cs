using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace PsuHistory.Data.EF.SQL.ContextFactory
{
    public abstract class DbContextFactoryBase
    {
        protected DbContextOptionsBuilder optionsBuilder;

        protected string GetConnectionString(string name)
        {
            // получаем конфигурацию из файла appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            // получаем строку подключения из файла appsettings.json
            return config.GetConnectionString(name);
        }
    }
}
