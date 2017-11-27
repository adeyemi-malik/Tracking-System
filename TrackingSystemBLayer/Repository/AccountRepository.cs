using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystemDLayer.DataModels;

namespace TrackingSystemBLayer.Repository
{
    public class AccountRepository<T> : IRepository<T, User<T>>
    {
        private Entities<T> context;

        public AccountRepository(Entities<T> entities)
        {
            context = entities;
        }
        public Task<User<T>> Add(User<T> entity)
        {
            context.Set<User<T>>().AddAsync(entity);
            SaveChanges();
            return Task.FromResult(entity);
        }

        public Task Delete(User<T> entity)
        {
            context.Remove(entity);
            SaveChanges();
            return Task.CompletedTask;
        }

        public Task<User<T>> FindByName(string name)
        {
            return context.Set<User<T>>().FirstOrDefaultAsync(s => s.NormalizedName == name);
        }

        public Task<User<T>> FindByEmail(string email)
        {
            return context.Set<User<T>>().FirstOrDefaultAsync(s => s.NormalizedEmail == email);
        }

        public Task Delete(T Id)
        {
            throw new NotImplementedException();
        }

        public Task<User<T>> Get(T Id)
        {
            return context.Set<User<T>>().FindAsync(Id);
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }

        public Task<User<T>> Update(User<T> entity)
        {
            context.Set<User<T>>().Update(entity);
            SaveChanges();
            return Task.FromResult(entity);
        }

        public Task<IQueryable<User<T>>> All()
        {
            return Task.FromResult(context.Set<User<T>>().AsQueryable());

        }

        public async Task RemoveFromRoleAsync(User<T> entity, string rolename)
        {          
            var role = entity.UserRoles.FirstOrDefault(r => r.Role.NormalizedName == rolename);
            entity.UserRoles.Remove(role);
            await SaveChanges();
        }

        public async Task<IEnumerable<User<T>>> GetUsersInRoleAsync(string rolename)
        {
            var role = await context.Set<Role<T>>().FirstOrDefaultAsync(r => r.NormalizedName == rolename);
            return role.UserRoles.Select(r => r.User);            
        }

        public Task<IEnumerable<Role<T>>> GetRoles(User<T> entity)
        {
            return Task.FromResult(entity.UserRoles.Select(r => r.Role));
        }

        public Task<IEnumerable<Role<T>>> GetRoles(T Id)
        {
            var roles = context.Set<User<T>>().Find(Id).UserRoles.Select(r => r.Role);
            return Task.FromResult(roles);
        }

        public async Task AddToRole(User<T> entity, string rolename)
        {
            var role = await context.Set<Role<T>>().FirstOrDefaultAsync(r => r.NormalizedName == rolename);
            var userrole = new UserRole<T> { RoleId = role.Id, UserId = entity.Id };
            entity.UserRoles.Add(userrole);
            await SaveChanges();
        }

        public async Task AddToRole(User<T> user, T Id)
        {
            var role = await context.Set<Role<T>>().FindAsync(Id);
            var userrole = new UserRole<T> {RoleId = role.Id, UserId = user.Id };
            user.UserRoles.Add(userrole);
            await SaveChanges();
        }

        public async Task AddToRole(T userId, T roleId)
        {
            var user = await Get(userId); ;
            var role = await context.Set<Role<T>>().FindAsync(roleId);
            var userrole = new UserRole<T> { RoleId = role.Id, UserId = user.Id };
            user.UserRoles.Add(userrole);
            await SaveChanges();
        }

    }
}
