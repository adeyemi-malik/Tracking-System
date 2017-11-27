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
    public class UserHelper<T> : IHelper<T>
    {
        AccountRepository<T> repository;
        public UserHelper(AccountRepository<T> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(IUser<T> iuser)
        {
            var user = convertIUser(iuser);
            await repository.Add(user);
            iuser.Id = user.Id;
        }

        public async Task DeleteAsync(IUser<T> iuser)
        {
            var user = await getUserAsync(iuser);
            await repository.Delete(user);
        }

        public async Task<IUser<T>> FindByIdAsync(T Id)
        {
            var user = await repository.Get(Id);
            var iuser = user != null ? convertUser(user) : null;
            return await Task.FromResult(iuser);

        }

        public async Task<IUser<T>> FindByNameAsync(string username)
        {
            var user = await repository.FindByName(username);
            var iuser = user != null ? convertUser(user) : null;
            return iuser;
        }

        public async Task UpdateAsync(IUser<T> iuser)
        {
            var user = await updateUserAsync(iuser);
            await repository.Update(user);
        }
      

        public IQueryable<IUser<T>> Users => repository.All().Result.Select(u => convertUser(u));
       

        public async Task AddToRoleAsync(IUser<T> iuser, string rolename)
        {
            var user = await getUserAsync(iuser);
            await repository.AddToRole(user, rolename);
        }

        public async Task RemoveFromRoleAsync(IUser<T> iuser, string roleName)
        {
            var user = await getUserAsync(iuser);
            await repository.RemoveFromRoleAsync(user, roleName);
        }

        public async Task<IList<string>> GetRolesAsync(IUser<T> iuser)
        {
            var user = await getUserAsync(iuser);
            return user.UserRoles.Select(r => r.Role.NormalizedName).ToList();          
        }

        public async  Task<IList<IUser<T>>> GetUsersInRoleAsync(string rolename)
        {
            var users =  await repository.GetUsersInRoleAsync(rolename);
            return users.Select(u => convertUser(u)).ToList();
        }

        public async Task<bool> IsInRoleAsync(IUser<T> iuser, string rolename)
        {
            var user = await getUserAsync(iuser);
            return user.UserRoles.Any(r => r.Role.NormalizedName == rolename);
        }

        public async Task SetPhoneNumberAsync(IUser<T> iuser, string phoneNumber)
        {
            var user = await getUserAsync(iuser);
            user.Phone = phoneNumber;
            await repository.SaveChanges();
        }

        public async Task<string> GetPhoneNumberAsync(IUser<T> iuser)
        {
            var user = await getUserAsync(iuser);
            return user.Phone;
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(IUser<T> iuser)
        {
            var user = getUserAsync(iuser);
            var confirmed = user.Result.PhoneConfirmed;
            return Task.FromResult(confirmed);
        }

        public async Task SetPhoneNumberConfirmedAsync(IUser<T> iuser, bool confirmed)
        {
            var user = await getUserAsync(iuser);
            user.PhoneConfirmed = confirmed;
            await repository.SaveChanges();
        }

        public async Task SetEmailAsync(IUser<T> iuser, string email)
        {
            var user = await getUserAsync(iuser);
            user.Email = email;
            await repository.SaveChanges();
        }

        public async Task<string> GetEmailAsync(IUser<T> iuser)
        {
            var user = await getUserAsync(iuser);
            return user.Email;
        }

        public Task<bool> GetEmailConfirmedAsync(IUser<T> iuser)
        {
            var user = getUserAsync(iuser);
            var confirmed = user.Result.EmailConfirmed;
            return Task.FromResult(confirmed);
        }

        public async Task SetEmailConfirmedAsync(IUser<T> iuser, bool confirmed)
        {
            var user = await getUserAsync(iuser);
            user.EmailConfirmed = confirmed;
            await repository.SaveChanges();
        }

        public async Task<IUser<T>> FindByEmailAsync(string email)
        {
            var user = await repository.FindByEmail(email);
            var iuser = user != null ? convertUser(user) : null;
            return iuser;
        }

        public Task SetPasswordHashAsync(IUser<T> iuser , string passwordHash)
        {
            return Task.FromResult(iuser.PasswordHash = passwordHash);
        }

        public Task<string> GetPasswordHashAsync(IUser<T> iuser)
        {
            var user = getUserAsync(iuser);
            var password = user.Result.Password;
            return Task.FromResult(password);
        }

        public Task<bool> HasPasswordAsync(IUser<T> iuser)
        {
            var user = getUserAsync(iuser);
            var password = user.Result.Password;
            var hasPassword = password != null ? true : false;
            return Task.FromResult(hasPassword);


        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(IUser<T> iuser)
        {
            var user = getUserAsync(iuser);
            var date = user.Result.LockOutEndDate;
            return Task.FromResult(date);
        }

        public async Task SetLockoutEndDateAsync(IUser<T> iuser, DateTimeOffset? lockoutEnd)
        {
            var user = await getUserAsync(iuser);
            user.LockOutEndDate = lockoutEnd;
            await repository.SaveChanges();
        }

        public async Task<int> IncrementAccessFailedCountAsync(IUser<T> iuser)
        {
            var user = await getUserAsync(iuser);
            var count = user.AccessFailedCount;
            count++;
            user.AccessFailedCount = count;
            await repository.SaveChanges();
            return count;
        }

        public async Task ResetAccessFailedCountAsync(IUser<T> iuser)
        {
            var user = await getUserAsync(iuser);
            user.AccessFailedCount = 0;
            await  repository.SaveChanges();
        }

        public Task<int> GetAccessFailedCountAsync(IUser<T> iuser)
        {
            var user = getUserAsync(iuser);
            var count = user.Result.AccessFailedCount;
            return Task.FromResult(count);
        }

        public Task<bool> GetLockoutEnabledAsync(IUser<T> iuser)
        {
            var user = getUserAsync(iuser);
            var lockout = user.Result.LockOutEnabled;
            return Task.FromResult(lockout);
        }

        public Task SetLockoutEnabledAsync(IUser<T> iuser, bool enabled)
        {
            return Task.FromResult(iuser.LockOutEnabled = enabled);
        }

        public async Task SetTwoFactorEnabledAsync(IUser<T> iuser, bool enabled)
        {
            var user = await getUserAsync(iuser);
            user.TwoFactorEnabled = enabled;
        }

        public async Task<bool> GetTwoFactorEnabledAsync(IUser<T> iuser)
        {
            var user = await getUserAsync(iuser);
            var twoFactor = user.TwoFactorEnabled;
            return twoFactor;
        }

        public IUser<T> convertUser(User<T> user)
        {
            var iuser = new IUser<T> { Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, OtherName = user.OtherName, Phone = user.Phone, PasswordHash = user.Password, UserName = user.UserName, NormalizedName = user.NormalizedName, NormalizedEmail = user.NormalizedEmail, TwoFactorEnabled = user.TwoFactorEnabled, AccessFailedCount = user.AccessFailedCount, EmailConfirmed = user.EmailConfirmed, LockOutEnabled = user.LockOutEnabled, LockOutEndDate = user.LockOutEndDate, PhoneConfirmed = user.PhoneConfirmed};
            return iuser;
        }

        public User<T> convertIUser(IUser<T> iuser)
        {
            var user = new User<T> { UserName = iuser.UserName, Password = iuser.PasswordHash, Phone = iuser.Phone, FirstName = iuser.FirstName, LastName = iuser.LastName, OtherName = iuser.OtherName, Email = iuser.Email, EmailConfirmed = iuser.EmailConfirmed, LockOutEnabled = iuser.LockOutEnabled, PhoneConfirmed = iuser.PhoneConfirmed, LockOutEndDate = DateTimeOffset.Now, AccessFailedCount = iuser.AccessFailedCount, NormalizedEmail = iuser.NormalizedEmail, NormalizedName = iuser.NormalizedName, TwoFactorEnabled = iuser.TwoFactorEnabled};
            return user;
        }

        public async Task<User<T>> getUserAsync(IUser<T> iuser)
        {
            return await repository.Get(iuser.Id);
        }

        public async Task<User<T>> updateUserAsync(IUser<T> iuser)
        {
            var user = await getUserAsync(iuser);
            user.FirstName = iuser.FirstName;
            user.LastName = iuser.LastName;
            user.OtherName = iuser.OtherName;
            user.Phone = iuser.Phone;
            user.UserName = iuser.UserName;
            user.Password = iuser.PasswordHash;
            user.Email = iuser.Email;
            user.NormalizedName = iuser.NormalizedName;
            user.NormalizedEmail = iuser.NormalizedEmail;
            user.AccessFailedCount = iuser.AccessFailedCount;
            user.EmailConfirmed = iuser.EmailConfirmed;
            user.LockOutEnabled = iuser.LockOutEnabled;
            user.LockOutEndDate = iuser.LockOutEndDate;
            user.PhoneConfirmed = iuser.PhoneConfirmed;
            user.TwoFactorEnabled = iuser.TwoFactorEnabled;
            return user;
        }
    }
}
