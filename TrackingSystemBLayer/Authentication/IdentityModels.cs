using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TrackingSystemBLayer.Authentication
{
    public partial class IUser<T>
    {
        public T Id { get; set; }

        public string UserName { get; set; }

        public string NormalizedName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string OtherName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public bool PhoneConfirmed { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockOutEndDate { get; set; }

        public bool LockOutEnabled { get; set; } = false;

        public int AccessFailedCount { get; set; }

        public DateTime SecurityStamp { get; set; }

        public string Phone { get; set; }
    }

    public class IRole<T>
    {
        public T Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }

}
