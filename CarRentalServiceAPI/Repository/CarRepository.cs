using CarRentalServiceAPI.Data;
using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Models;

namespace CarRentalServiceAPI.Repository
{
    public class CarRepository : ICarRepository
    {
        private DbConnectionContext _dbContext;

        public CarRepository(DbConnectionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CarDto> Create(CarDto carDto, Base64FormattingOptions Image)
        {
            var newCar = new Car()
            {
                CarId = Guid.NewGuid(),
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
                Image = Image,
                ReleaseDate = carDto.ReleaseDate,
                Popularity = 0,
                LastTimeModified = DateTime.Now,
            };

            await _dbContext.Cars.AddAsync(newCar);

            return carDto;
        }

        public async Task<bool> Delete(string carId)
        {
            var car = await _dbContext.Cars.FindAsync(carId);

            if (car == null)
                throw new BadHttpRequestException("There is no car witch such Id");

            _dbContext.Cars.Remove(car);
            return true;
        }

        public async Task<CarDto> Get(string carId)
        {
            var car = await _dbContext.Cars.FindAsync(carId);
            if (car == null)
                throw new BadHttpRequestException("There is no car witch such Id");

            return new CarDto()
            {
                //dodać pola
            };
        }

        public async Task<CarDto> Update(CarDto carDto)
        {            
            var currentCar = await _dbContext.Cars.FindAsync(carDto.CarId);
            if (currentCar == null)
                throw new BadHttpRequestException("There is no car with such Id.");

            //dodać logikę updatowania danych aut

            return carDto;
        }
    }
}
