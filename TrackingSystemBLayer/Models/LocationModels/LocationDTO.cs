using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingSystemBLayer.Models.DeviceModels;

namespace TrackingSystemBLayer.Models.LocationModels
{
    public class LocationDTO<T>
    {
        public T Id { get; set; }
        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

        public T DeviceId { get; set; }
    }
}
