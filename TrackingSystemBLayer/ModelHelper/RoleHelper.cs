using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystemBLayer.Authentication;
using TrackingSystemBLayer.Repository;
using TrackingSystemDLayer.DataModels;

namespace TrackingSystemBLayer.ModelHelper
{
    public class RoleHelper<T> : IHelper<T>
    {
        RoleRepository<T> repository;

        public RoleHelper(RoleRepository<T> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(IRole<T> irole)
        {
            var role = new Role<T> { Name = irole.Name, NormalizedName = irole.NormalizedName};
            await repository.Add(role);
        }

        public async Task DeleteAsync(IRole<T> irole)
        {
            var role = await getRoleAsync(irole);
            await repository.Delete(role);
        }

        public Task<IRole<T>> FindByIdAsync(T roleId)
        {
            var role =  repository.Get(roleId);
            var irole = role.Result != null ? convertRole(role.Result) : null;
            return Task.FromResult< IRole <T>> (irole);
        }

        public Task<IRole<T>> FindByNameAsync(string rolename)
        {
            var role = repository.FindByName(rolename);
            var irole = role.Result != null ? convertRole(role.Result) : null;
            return Task.FromResult(irole);
        }

        public async Task UpdateAsync(IRole<T> irole)
        {
            var role = await updateRoleAsync(irole);
            await repository.Update(role);
        }

        public IQueryable<IRole<T>> Roles => repository.All().Result.Select(r => convertRole(r));


        public IRole<T> convertRole(Role<T> role)
        {
            var irole = new IRole<T> { Id = role.Id, Name = role.Name, NormalizedName = role.NormalizedName};
            return irole;
        }

        public Role<T> convertIRole(IRole<T> irole)
        {
            var role = new Role<T> { Id = irole.Id, Name = irole.Name, NormalizedName = irole.NormalizedName};
            return role;
        }

        public async Task<Role<T>> getRoleAsync(IRole<T> irole)
        {
            return await repository.Get(irole.Id);
        }

        public async Task<Role<T>> updateRoleAsync(IRole<T> irole)
        {
            var role = await getRoleAsync(irole);
            role.Name = irole.Name;
            role.NormalizedName = irole.NormalizedName;
            return role;
        }
    }
}
