using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystemBLayer.Models.DeviceModels;
using TrackingSystemBLayer.Repository;
using TrackingSystemDLayer.DataModels;

namespace TrackingSystemBLayer.ModelHelper
{
    public class DeviceHelper<T> : IHelper<T>
    {
        DeviceRepository<T> repository;

        public DeviceHelper(DeviceRepository<T> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(DeviceDTO<T> deviceDto)
        {
            var device = new Device<T> { Name = deviceDto.Name, Status = deviceDto.Status, UserId = deviceDto.UserId };
            await repository.Add(device);
        }

        public async Task DeleteAsync(DeviceDTO<T> deviceDTO)
        {
            var device = await GetDeviceAsync(deviceDTO);
            await repository.Delete(device);
        }

        public Task<DeviceDTO<T>> FindByIdAsync(T deviceId)
        {
            var device = repository.Get(deviceId);
            var deviceDTO = device.Result != null ? convertDevice(device.Result) : null;
            return Task.FromResult(deviceDTO);
        }

        public Task<DeviceDTO<T>> FindByNameAsync(string deviceName)
        {
            var device = repository.FindByName(deviceName);
            var deviceDTO = device.Result != null ? convertDevice(device.Result) : null;
            return Task.FromResult(deviceDTO);
        }

        public async Task UpdateAsync(DeviceDTO<T> deviceDTO)
        {
            var device = await UpdateDeviceAsync(deviceDTO);
            await repository.Update(device);
        }

        public IQueryable<DeviceDTO<T>> Devices => repository.All().Result.Select(d => convertDevice(d));


        public DeviceDTO<T> convertDevice(Device<T> device)
        {
            var deviceDTO = new DeviceDTO<T> { Id = device.Id, Name = device.Name, UserId = device.UserId, Status = device.Status };
            return deviceDTO;
        }

        public Device<T> ConvertDeviceDTO(DeviceDTO<T> deviceDTO)
        {
            var device = new Device<T> { Id = deviceDTO.Id, Name = deviceDTO.Name, UserId = deviceDTO.UserId, Status = deviceDTO.Status };
            return device;
        }

        public async Task<Device<T>> GetDeviceAsync(DeviceDTO<T> deviceDTO)
        {
            return await repository.Get(deviceDTO.Id);
        }

        public async Task<Device<T>> UpdateDeviceAsync(DeviceDTO<T> deviceDTO)
        {
            var device = await GetDeviceAsync(deviceDTO);
            device.Name = device.Name;
            device.UserId = deviceDTO.UserId;
            device.Status = deviceDTO.Status;
            return device;
        }
    }
}
