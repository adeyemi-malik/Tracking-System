using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrackingSystemDLayer.DataModels
{
    public class UserRole<T> : IEntity<T>
    {
        public T Id { get; set; }

        [Timestamp, DataType("timestamp")]
        public byte[] TimeStamp { get; set; }

        public T UserId { get; set; } 
        public T RoleId { get; set; }

        public User<T> User { get; set; }

        public Role<T> Role { get; set; }
    }
}
