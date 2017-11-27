using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TrackingSystemDLayer.DataModels;

namespace TrackingSystemBLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Entities<int>>(options =>
                    options.UseSqlServer(connectionString));
        }
    }
}
