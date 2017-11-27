using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystemBLayer.Models.DeviceModels;
using TrackingSystemBLayer.Models.LocationModels;
using TrackingSystemBLayer.Repository;
using TrackingSystemDLayer.DataModels;

namespace TrackingSystemBLayer.ModelHelper
{
    public class LocationHelper<T> : IHelper<T>
    {
        LocationRepository<T> repository;

        public LocationHelper(LocationRepository<T> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(LocationDTO<T> locationDTO)
        {
            var location = new Location<T> { Longitude = locationDTO.Longitude, Latitude = locationDTO.Latitude, Date = locationDTO.Date, DeviceId = locationDTO.DeviceId };
            await repository.Add(location);
        }

        public async Task DeleteAsync(LocationDTO<T> locationDTO)
        {
            var location = await GetLocationAsync(locationDTO);
            await repository.Delete(location);
        }

        public Task<LocationDTO<T>> FindByIdAsync(T locationId)
        {
            var location = repository.Get(locationId);
            var locationDTO = location.Result != null ? ConvertLocation(location.Result) : null;
            return Task.FromResult(locationDTO);
        }

        public async Task<IQueryable<LocationDTO<T>>> FindByDateAsync(DateTimeOffset dateTimeOffset)
        {
            var locations = await repository.FindByDate(dateTimeOffset);
            return locations.Select(l => ConvertLocation(l));           
        }

        public async Task<IQueryable<LocationDTO<T>>> FindByLongitudeAsync(string longitude)
        {
            var locations = await repository.FindByLongitude(longitude);
            return locations.Select(l => ConvertLocation(l));
        }

        public async Task<IQueryable<LocationDTO<T>>> FindByLatitudeAsync(string latitude)
        {
            var locations = await repository.FindByLongitude(latitude);
            return locations.Select(l => ConvertLocation(l));
        }

        public async Task UpdateAsync(LocationDTO<T> locationDTO)
        {
            var location = await UpdateLocationAsync(locationDTO);
            await repository.Update(location);
        }

        public IQueryable<LocationDTO<T>> Locations => repository.All().Result.Select(d => ConvertLocation(d));


        public LocationDTO<T> ConvertLocation(Location<T> location)
        {
            var locationDTO  = new LocationDTO<T> { Id = location.Id, Longitude = location.Longitude, Latitude = location.Latitude, DeviceId = location.DeviceId};
            return locationDTO;
        }

        public Location<T> ConvertLocationDTO(LocationDTO<T> locationDTO)
        {
            var location = new Location<T> { Id = locationDTO.Id, Latitude = locationDTO.Latitude, Longitude = locationDTO.Longitude, DeviceId = locationDTO.DeviceId};
            return location;
        }

        public async Task<Location<T>> GetLocationAsync(LocationDTO<T> locationDTO)
        {
            return await repository.Get(locationDTO.Id);
        }

        public async Task<Location<T>> UpdateLocationAsync(LocationDTO<T> locationDTO)
        {
            var location = await GetLocationAsync(locationDTO);
            location.Latitude = locationDTO.Latitude;
            location.Longitude = locationDTO.Longitude;
            location.Date = locationDTO.Date;
            location.DeviceId = locationDTO.DeviceId;
            return location;
        }
    }
}
