using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TrackingSystemDLayer.DataModels
{
    public partial class UserClaim<T> : IEntity<T>
    {

        [Key]
        public T Id { get; set; }

        [Required]
        public T UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        [Timestamp, DataType("timestamp")]
        public byte[] TimeStamp { get; set; }

        [ForeignKey("UserId")]
        public virtual User<T> User { get; set; }

    }
}
