using System;
using System.Collections.Generic;
using System.Text;

namespace TrackingSystemDLayer.DataModels
{
    public interface IEntity<T>
    {
       T Id { get; set; }

       byte[] TimeStamp { get; set; }
    }
}
