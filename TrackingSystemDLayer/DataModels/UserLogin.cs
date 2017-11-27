using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TrackingSystemDLayer.DataModels
{
    public class UserLogin<T> : IEntity<T>
    {
        [Key]
        [Column(Order = 3)]
        public T Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(maximumLength: 256, MinimumLength = 1)]
        public string LoginProvider { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(maximumLength: 256, MinimumLength = 1)]
        public string ProviderKey { get; set; }

        [Timestamp, DataType("timestamp")]
        public byte[] TimeStamp { get; set; }

        [ForeignKey("Id")]
        public virtual  User<T> User {get; set;}
    }
}
