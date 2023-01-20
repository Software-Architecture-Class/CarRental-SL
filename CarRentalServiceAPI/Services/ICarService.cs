using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Data.Response;

namespace CarRentalServiceAPI.Services
{
    public interface ICarService
    {
        public Task<CarDto> CreateCar(CarDto carDto);
        public Task<CarResponse> GetCar(string carId);
        public Task<bool> DeleteCar(string carId);
        public Task<bool> UpdateCarRecord(CarDto carDto);
        public Task<List<CarResponse>> GetListOfCars(Filter filter);
    }
}
