using CarRentalServiceAPI.Data.Dto;

namespace CarRentalServiceAPI.Services
{
    public interface ICarService
    {
        public Task<CarDto> CreateCar(CarDto carDto, IFormFile Image);
    }
}
