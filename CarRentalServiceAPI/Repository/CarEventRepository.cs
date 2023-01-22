using CarRentalServiceAPI.Data;
using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Data.Response;
using CarRentalServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Repository
{
    public class CarEventRepository : ICarEventRepository
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

        public async Task<bool> Delete(string transactionId)
        {
            var carEvent = await _context.CarEvents.FirstOrDefaultAsync(x => x.TransactionId == transactionId);

            if (carEvent == null)
                throw new BadHttpRequestException("There is no such reservation");

            _context.CarEvents.Remove(carEvent);
            _context.SaveChanges();

            return true;
        }

        public async Task<List<CarEvent>> GetAllForRequestedCar(string carId)
        {
            var allCarEvents = await _context.CarEvents.Where(x => x.CarId == carId).ToListAsync();


            if (allCarEvents.Any())
                return allCarEvents;
            else
                return new List<CarEvent>();
        }

        public async Task<CarEventDto> Update(CarEventDto carEventDto)
        {
            if (string.IsNullOrEmpty(carEventDto.TransactionId))
                throw new BadHttpRequestException("You need to input the transactionId if you want to update it");

            var carReservation = await _context.CarEvents.FirstOrDefaultAsync(x => x.TransactionId == carEventDto.TransactionId);
            if (carReservation == null)
                throw new BadHttpRequestException("There is no transaction with this Id");

            if (carEventDto.startDate != null) carReservation.startDate = carEventDto.startDate.Value;
            if (carEventDto.finishDate != null) carReservation.finishDate = carEventDto.finishDate.Value;
            if (carEventDto.Rate != null) carReservation.Rate = carEventDto.Rate.Value;

            _context.SaveChanges();

            return carEventDto;
        }

        public async Task<CarReservationResponse> Get(string transactionId)
        {
            var carReservation = await _context.CarEvents.FirstOrDefaultAsync(x => x.TransactionId == transactionId);

            if (carReservation == null)
                throw new BadHttpRequestException("There is no transaction with that Id");

            var carReservationResponse = new CarReservationResponse()
            {
                ReservationId = carReservation.TransactionId ?? string.Empty,
                CarId = carReservation.CarId ?? string.Empty,
                startDate = carReservation.startDate,
                finishDate = carReservation.finishDate,
                paidPrice = carReservation.paidPrice ?? 0.0,
                Rate = carReservation.Rate ?? 0,
                DiscountPercentage = carReservation.DiscountPercentage ?? 0
            };

            return carReservationResponse;
        }

        public async Task<List<CarReservationResponse>> GetAllForUser(string userId)
        {
            var allUserHistory = await _context.CarEvents.Where(x => x.UserId == userId).ToListAsync();

            var allUserTransactionsResponse = new List<CarReservationResponse>();
            foreach(var transaction in allUserHistory)
            {
                var transactionResponse = new CarReservationResponse()
                {
                    ReservationId = transaction.TransactionId ?? string.Empty,
                    CarId = transaction.CarId ?? string.Empty,
                    startDate = transaction.startDate,
                    finishDate = transaction.finishDate,
                    paidPrice = transaction.paidPrice ?? 0.0,
                    Rate = transaction.Rate ?? 0,
                    DiscountPercentage = transaction.DiscountPercentage ?? 0
                };

                allUserTransactionsResponse.Add(transactionResponse);
            }

            return allUserTransactionsResponse;
        }
    }
}
