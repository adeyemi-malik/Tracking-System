using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingSystem.Models.LocationViewModels
{
    public class Location
    {
        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

        public int DeviceId { get; set; }
    }
}
