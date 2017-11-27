using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackingSystemDLayer.DataModels;
using System.Linq;

namespace TrackingSystemBLayer.Repository
{
    public class RoleRepository<T> : IRepository<T, Role<T>>
    {
        private Entities<T> context;

        public RoleRepository(Entities<T> entities)
        {
            context = entities;
        }
        public Task<Role<T>> Add(Role<T> entity)
        {
            context.Set<Role<T>>().AddAsync(entity);
            SaveChanges();
            return Task.FromResult(entity);
        }

        public Task Delete(Role<T> entity)
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

        public Task<Role<T>> Get(T Id)
        {
            return context.Set<Role<T>>().FindAsync(Id);
        }

        public Task<Role<T>> Update(Role<T> entity)
        {
            context.Set<Role<T>>().Update(entity);
            SaveChanges();
            return Task.FromResult(entity);

        }

        public Task<Role<T>> FindByName(string name)
        {
            return context.Set<Role<T>>().FirstOrDefaultAsync(s => s.NormalizedName == name);
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }

        public Task<IQueryable<Role<T>>> All()
        {
            return Task.FromResult(context.Set<Role<T>>().AsQueryable());
        }
    }
}
