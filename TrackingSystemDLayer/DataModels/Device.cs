using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TrackingSystemDLayer.DataModels
{
    public class Device<T> : IEntity<T>
    {
        public Device()
        {
            Locations = new HashSet<Location<T>>();
        }
        public T Id { get; set; }

        [Required]
        [StringLength(maximumLength: 256, MinimumLength = 3)]
        public string Name { get; set; }

        public T UserId { get; set; }

        public bool Status { get; set; }

        [ForeignKey("UserId")]
        public User<T> User { get; set; }

        [Timestamp, DataType("timestamp")]
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<Location<T>> Locations { get; set; }

    }
}
