using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrackingSystemDLayer.DataModels
{

    public partial class User<T> : IEntity<T>
    {

        public User()
        {
            UserRoles = new HashSet<UserRole<T>>();
            Claims = new HashSet<UserClaim<T>>();
            Logins = new HashSet<UserLogin<T>>();
            Devices = new HashSet<Device<T>>();
        }

        [Key]
        public T Id { get; set; }

        //[Index(IsUnique = true)]
        [Required]
        [StringLength(maximumLength: 256, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 256, MinimumLength = 3)]
        public string NormalizedName { get; set; }

        [Required]
        [StringLength(maximumLength: 256, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 256, MinimumLength = 3)]
        public string LastName { get; set; }

        [StringLength(maximumLength: 256)]
        public string OtherName { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public bool PhoneConfirmed { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [EmailAddress]
        public string NormalizedEmail { get; set; }

        [Required]
        public bool EmailConfirmed { get; set; }

        [Required]
        public bool TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockOutEndDate { get; set; }

        [Required]
        public bool LockOutEnabled { get; set; }

        [Required]
        public int AccessFailedCount { get; set; }

        [Timestamp, DataType("timestamp")]
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<UserRole<T>> UserRoles { get; set; }
        public virtual ICollection<UserLogin<T>> Logins { get; set; }
        public virtual ICollection<UserClaim<T>> Claims { get; set; }
        public virtual ICollection<Device<T>> Devices { get; set; }



    }

}
