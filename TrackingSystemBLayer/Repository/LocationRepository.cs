using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystemDLayer.DataModels;

namespace TrackingSystemBLayer.Repository
{
    public class LocationRepository<T> : IRepository<T, Location<T>>
    {
        private Entities<T> context;

        public LocationRepository(Entities<T> entities)
        {
            context = entities;
        }
        public Task<Location<T>> Add(Location<T> entity)
        {
            context.Set<Location<T>>().AddAsync(entity);
            SaveChanges();
            return Task.FromResult(entity);
        }

        public Task Delete(Location<T> entity)
        {
            context.Remove(entity);
            SaveChanges();
            return Task.CompletedTask;
        }

        public Task Delete(T Id)
        {
            throw new NotImplementedException();
            SaveChanges();
        }

        public Task<Location<T>> Get(T Id)
        {
            return context.Set<Location<T>>().FindAsync(Id);
        }

        public Task<Location<T>> Update(Location<T> entity)
        {
            context.Set<Location<T>>().Update(entity);
            SaveChanges();
            return Task.FromResult(entity);

        }

        public Task<IQueryable<Location<T>>> FindByLongitude(string longitude)
        {
            return Task.FromResult(context.Set<Location<T>>().Where(s => s.Longitude == longitude));
        }

        public Task<IQueryable<Location<T>>> FindByDate(DateTimeOffset dateTimeOffset)
        {
            return Task.FromResult(context.Set<Location<T>>().Where(s => s.Date == dateTimeOffset));
        }

        public Task<IQueryable<Location<T>>> FindByLatitude(string latitude)
        {
            return Task.FromResult(context.Set<Location<T>>().Where(s => s.Latitude == latitude));
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }

        public Task<IQueryable<Location<T>>> All()
        {
            return Task.FromResult(context.Set<Location<T>>().AsQueryable());
        }
    }
}
