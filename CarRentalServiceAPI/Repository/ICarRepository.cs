using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Data.Response;
using CarRentalServiceAPI.Models;

namespace CarRentalServiceAPI.Repository
{
    public interface ICarRepository
    {
        public Task<bool> Create(Car carDto);
        public Task<bool> Update(CarDto carDto);
        public Task<bool> Delete(string carId);
        public Task<Car> Get(string carId);
        public Task<List<CarResponse>> GetAll();
    }
}
