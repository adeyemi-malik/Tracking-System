using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrackingSystemDLayer.DataModels
{
    public partial class Role<T> : IEntity<T>
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole<T>>();
        }

        [Key]
        public T Id { get; set; }

        [Required]
        [StringLength(maximumLength: 256, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 256, MinimumLength = 3)]
        public string NormalizedName { get; set; }

        [Timestamp, DataType("timestamp")]
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<UserRole<T>> UserRoles { get; set; }

    }
}
