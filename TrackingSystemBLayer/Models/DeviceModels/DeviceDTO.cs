using System;
using System.Collections.Generic;
using System.Text;

namespace TrackingSystemBLayer.Models.DeviceModels
{
    public class DeviceDTO<T>
    {
        public T Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public T UserId { get; set; }
    }
}
