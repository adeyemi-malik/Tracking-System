using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TrackingSystemDLayer.DataModels;

namespace TrackingSystemDLayer.Factory
{
    class ContextFactory
    {
    }

    public class EntittiesContextFactory<T> : IDesignTimeDbContextFactory<Entities<T>>
    {
        //public Entities Create(DbContextFactoryOptions options)
        //{
        //    var builder = new DbContextOptionsBuilder<Entities>();
        //    builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Entities;Trusted_Connection=True;MultipleActiveResultSets=true",
        //        optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(Entities).GetTypeInfo().Assembly.GetName().Name));
        //    return new Entities(builder.Options);
        //}

        public Entities<T> CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<Entities<T>>();

            var connectionString = configuration.GetConnectionString("Entities");

            builder.UseSqlServer(connectionString);

            return new Entities<T>(builder.Options);
        }
    }


}
