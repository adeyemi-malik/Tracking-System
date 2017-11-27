using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TrackingSystemDLayer.DataModels
{
    public class Location<T> : IEntity<T>
    {
        [Key]
        public T Id { get; set; }

        [Required]
        [StringLength(maximumLength: 256, MinimumLength = 3)]
        public string Longitude { get; set; }

        [Required]
        public string Latitude { get; set; }

        public DateTimeOffset Date { get; set; }

        public T DeviceId { get; set; }

        [ForeignKey("DeviceId")]
        public Device<T> Device { get; set; }

        [Timestamp, DataType("timestamp")]
        public byte[] TimeStamp { get; set; }
    }
}
