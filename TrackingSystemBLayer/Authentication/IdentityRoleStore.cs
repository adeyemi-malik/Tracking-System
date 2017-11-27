using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using TrackingSystemBLayer.Repository;
using TrackingSystemBLayer.ModelHelper;

namespace TrackingSystemBLayer.Authentication
{
    public class IdentityRoleStore<T> :  IRoleStore<IRole<T>>, IQueryableRoleStore<IRole<T>>
    {
        private RoleHelper<T> helper;
        public IdentityRoleStore(RoleHelper<T> helper)
        {
            this.helper = helper;
        }
        public async Task<IdentityResult> CreateAsync(IRole<T> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            await helper.CreateAsync(role);

            return IdentityResult.Success;

        }

        public async Task<IdentityResult> DeleteAsync(IRole<T> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            await helper.DeleteAsync(role);

            return IdentityResult.Success;
        }

        public Task<IRole<T>> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            return helper.FindByIdAsync((T)Convert.ChangeType(roleId, typeof(T)));
        }

        public  Task<IRole<T>> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
           
            return helper.FindByNameAsync(normalizedRoleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(IRole<T> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            var name = role.NormalizedName;
            return Task.FromResult(name);
        }

        public Task<string> GetRoleIdAsync(IRole<T> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            var Id = role.Id.ToString();

            return Task.FromResult(Id);
        }

        public Task<string> GetRoleNameAsync(IRole<T> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            var name = role.Name;

            return Task.FromResult(name);
        }

        public Task SetNormalizedRoleNameAsync(IRole<T> role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(IRole<T> role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.Name = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(IRole<T> role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            await helper.UpdateAsync(role);

            return IdentityResult.Success;
        }

        public IQueryable<IRole<T>> Roles => helper.Roles;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    helper = null;
                }

                disposedValue = true;
            }
        }

       
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            
        }
        #endregion


    }
}
