using CarRentalServiceAPI.Data;
using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Data.Response;
using CarRentalServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Repository
{
    public class CarRepository : ICarRepository
    {
        private DbConnectionContext _dbContext;

        public CarRepository(DbConnectionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Car newCar)
        {
            await _dbContext.Cars.AddAsync(newCar);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(string carId)
        {
            var car = await _dbContext.Cars.FindAsync(carId);

            if (car == null)
                throw new BadHttpRequestException("There is no car witch such Id");

            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Car> Get(string carId)
        {
            var carFromDb = await _dbContext.Cars.FindAsync(carId);
            if (carFromDb == null)
                throw new BadHttpRequestException("There is no car witch such Id");

            return carFromDb;
        }

        public async Task<bool> Update(CarDto carDto)
        {            
            var currentCar = await _dbContext.Cars.FindAsync(carDto.CarId);
            if (currentCar == null)
                throw new BadHttpRequestException("There is no car with such Id.");

            if (!string.IsNullOrEmpty(carDto.Brand)) currentCar.Brand = carDto.Brand;
            if (!string.IsNullOrEmpty(carDto.Model)) currentCar.Model = carDto.Model;
            if (carDto.Power != null) currentCar.Power = carDto.Power.Value;
            if (carDto.Acceleration != null) currentCar.Acceleration = carDto.Acceleration.Value;
            if (carDto.gearboxType != null) currentCar.gearboxType = carDto.gearboxType.Value;
            if (carDto.Drive != null) currentCar.Drive = carDto.Drive.Value;
            if (carDto.Description != null) currentCar.Description = carDto.Description;
            if (carDto.carCategory != null) currentCar.carCategory = carDto.carCategory.Value;
            if (string.IsNullOrEmpty(carDto.Description)) currentCar.Description = carDto.Description;
            if (carDto.Price != null) currentCar.Price = carDto.Price.Value;
            if (carDto.PeopleCapacity != null) currentCar.peopleCapacity = carDto.PeopleCapacity.Value;
            if (carDto.EngineCapacity != null) currentCar.EngineCapacity = carDto.EngineCapacity.Value;
            if (carDto.TimeFrom0To100 != null) currentCar.TimeFrom0To100 = carDto.TimeFrom0To100.Value;
            if (carDto.Image != null)
            {
                var carImage = carDto.Image;
                var base64VersionImage = string.Empty;
                using (var ms = new MemoryStream())
                {
                    carImage.CopyTo(ms);
                    var imageBytes = ms.ToArray();
                    base64VersionImage = Convert.ToBase64String(imageBytes);
                }
                currentCar.Image = base64VersionImage;
            }
            if (carDto.ReleaseDate != null) currentCar.ReleaseDate = carDto.ReleaseDate.Value;
            currentCar.LastTimeModified = DateTime.Now;

            _dbContext.SaveChanges();

            return true;
        }

        public async Task<List<CarResponse>> GetAll()
        {
            var carsList = (await _dbContext.Cars.ToArrayAsync()).ToList();

            var carsResponseList = new List<CarResponse>();
            foreach(var Car in carsList)
            {
                var carResponse = new CarResponse()
                {
                    CarId = Car.CarId,
                    Brand = Car.Brand,
                    Model = Car.Model,
                    Power = Car.Power,
                    Acceleration = Car.Acceleration,
                    gearboxType = Car.gearboxType,
                    Drive = Car.Drive,
                    carCategory = Car.carCategory,
                    Description = Car.Description,
                    Price = Car.Price,
                    peopleCapacity = Car.peopleCapacity,
                    Image = Car.Image,
                    ReleaseDate = Car.ReleaseDate,
                    Popularity = Car.Popularity,
                    EngineCapacity = Car.EngineCapacity,
                    TimeForm0To100 = Car.TimeFrom0To100
                };

                carsResponseList.Add(carResponse);
            }

            return carsResponseList;
        }

    }
}
