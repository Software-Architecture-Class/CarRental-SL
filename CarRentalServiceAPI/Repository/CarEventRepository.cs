using CarRentalServiceAPI.Data;
using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Repository
{
    public class CarEventRepository: ICarEventRepository
    {
        private readonly DbConnectionContext _context;

        public CarEventRepository(DbConnectionContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(CarEvent carEvent)
        {
            await _context.CarEvents.AddAsync(carEvent);

            _context.SaveChanges();

            return true;
        }

        public async Task<List<CarEvent>> GetAllForRequestedCar(string carId)
        {
            var allCarEvents = await _context.CarEvents.Where(x => x.CarId == carId).ToListAsync();

             
            if(allCarEvents.Any())
                return allCarEvents;
            else
                return new List<CarEvent>();
        }     
    }
}
