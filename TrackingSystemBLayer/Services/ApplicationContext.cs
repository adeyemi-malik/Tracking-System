using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TrackingSystemDLayer.DataModels;

namespace TrackingSystemBLayer.Services
{
    public class ApplicationContext<T> : Entities<T>
    {
        public ApplicationContext(DbContextOptions<Entities<T>> options) : base(options)
        {

        }
    }
}
