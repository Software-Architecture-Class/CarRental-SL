using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Data.Response;
using CarRentalServiceAPI.Models;
using CarRentalServiceAPI.Repository;
using SortingOptions = CarRentalServiceAPI.Data.Dto.Filter.SortingOptions;

namespace CarRentalServiceAPI.Services
{
    public class CarService: ICarService
    {
        private ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<CarDto> CreateCar(CarDto carDto)
        {
            var carImage = carDto.Image;
            var base64VersionImage = string.Empty;
            if(carImage != null)
                using (var ms = new MemoryStream())
                {
                    carImage.CopyTo(ms);
                    var imageBytes = ms.ToArray();
                    base64VersionImage = Convert.ToBase64String(imageBytes);
                }

            var newCar = new Car()
            {
                CarId = Guid.NewGuid().ToString(),
                Brand = carDto.Brand,
                Model = carDto.Model,
                Power = carDto.Power,
                Acceleration = carDto.Acceleration,
                gearboxType = carDto.gearboxType,
                Drive = carDto.Drive,
                carCategory = carDto.carCategory,
                Description = carDto.Description,
                Price = carDto.Price,
                peopleCapacity = carDto.PeopleCapacity,
                Image = base64VersionImage,
                ReleaseDate = carDto.ReleaseDate,
                Popularity = 0,
                LastTimeModified = DateTime.Now,
                EngineCapacity = carDto.EngineCapacity,
            };

            await _carRepository.Create(newCar);
            
            return carDto;
        }  
        
        public async Task<CarResponse> GetCar(string carId)
        {
            var carFromDb = await _carRepository.Get(carId);

            if (carFromDb == null)
                throw new BadHttpRequestException("There is no car with such Id");                        

            var carResponse = new CarResponse()
            {
                CarId = carId,
                Brand = carFromDb.Brand,
                Model = carFromDb.Model,
                Power = carFromDb.Power,
                Acceleration = carFromDb.Acceleration,
                gearboxType = carFromDb.gearboxType,
                Drive = carFromDb.Drive,
                carCategory = carFromDb.carCategory,
                Description=carFromDb.Description,
                Price=carFromDb.Price,
                peopleCapacity =carFromDb.peopleCapacity,
                ReleaseDate=carFromDb.ReleaseDate,
                Image = carFromDb.Image,
                Popularity=carFromDb.Popularity,
            };

            return carResponse;
        }

        public async Task<bool> DeleteCar(string carId)
        {
            var carFromDb = await _carRepository.Get(carId);
            if (carFromDb == null)
                throw new BadHttpRequestException("There is no car record with such carId");

            await _carRepository.Delete(carId);

            return true;
        }

        public async Task<bool> UpdateCarRecord(CarDto newCarData)
        {
            if (newCarData.CarId == null)
                throw new BadHttpRequestException("You must input carId if you want to update record.");

            var carFromDb = await _carRepository.Get(newCarData.CarId);

            if (carFromDb == null)
                throw new BadHttpRequestException("There is no car record with such carId");

            await _carRepository.Update(newCarData);

            return true;
        }

        public async Task<List<CarResponse>> GetListOfCars(Filter filter)
        {
            var carsResponseList = await _carRepository.GetAll();

            switch (filter.Option)
            {
                case SortingOptions.PriceHighToLow:
                    return carsResponseList.OrderByDescending(x => x.Price).ToList();

                case SortingOptions.PriceLowToHigh:
                    return carsResponseList.OrderBy(x => x.Price).ToList();

                case SortingOptions.ReleaseDate:
                    return carsResponseList.OrderByDescending(x => x.ReleaseDate).ToList();

                case SortingOptions.Acceleration:
                    return carsResponseList.OrderByDescending(x => x.Acceleration).ToList();

                case SortingOptions.Popularity:
                    return carsResponseList.OrderByDescending(x => x.Popularity).ToList();

                default: break;
            }

            return carsResponseList;
        }

    }
}
