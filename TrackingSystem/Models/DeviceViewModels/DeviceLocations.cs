using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingSystem.Models.LocationViewModels;

namespace TrackingSystem.Models.DeviceViewModels
{
    public class DeviceLocations
    {
        public int DeviceId { get; set; }

        public string Name { get; set; }

        public IEnumerable<Location> Locations { get; set; }
        
    }
}
