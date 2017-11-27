using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingSystemBLayer.Models.LocationModels;


namespace TrackingSystemBLayer.Models.DeviceModels
{
    public class DeviceLocations<T>
    {
        public DeviceDTO<T> Device { get; set; }

        public IEnumerable<LocationDTO<T>> Locations { get; set; }
        
    }
}
