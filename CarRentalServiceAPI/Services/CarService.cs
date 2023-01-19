using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Repository;

namespace CarRentalServiceAPI.Services
{
    public class CarService: ICarService
    {
        private ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<CarDto> CreateCar(CarDto car, IFormFile Image)
        {
            //konwertowanie IFormImage na base64 lub byte[]

            Base64FormattingOptions Image64 = Base64FormattingOptions.None;
            await _carRepository.Create(car, Image64);
            
            return car;
        }
    }
}
