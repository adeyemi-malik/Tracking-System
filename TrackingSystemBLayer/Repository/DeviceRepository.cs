using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystemDLayer.DataModels;

namespace TrackingSystemBLayer.Repository
{
    public class DeviceRepository<T> : IRepository<T, Device<T>>
    {
        private Entities<T> context;

        public DeviceRepository(Entities<T> entities)
        {
            context = entities;
        }
        public Task<Device<T>> Add(Device<T> entity)
        {
            context.Set<Device<T>>().AddAsync(entity);
            SaveChanges();
            return Task.FromResult(entity);
        }

        public Task Delete(Device<T> entity)
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

        public Task<Device<T>> Get(T Id)
        {
            return context.Set<Device<T>>().FindAsync(Id);
        }

        public Task<Device<T>> Update(Device<T> entity)
        {
            context.Set<Device<T>>().Update(entity);
            SaveChanges();
            return Task.FromResult(entity);

        }

        public Task<Device<T>> FindByName(string name)
        {
            return context.Set<Device<T>>().FirstOrDefaultAsync(s => s.Name == name);
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }

        public Task<IQueryable<Device<T>>> All()
        {
            return Task.FromResult(context.Set<Device<T>>().AsQueryable());
        }
    }
}
