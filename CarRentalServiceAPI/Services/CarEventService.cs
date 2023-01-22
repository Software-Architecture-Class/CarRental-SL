using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Data.Response;
using CarRentalServiceAPI.Models;
using CarRentalServiceAPI.Repository;
using DiscountType = CarRentalServiceAPI.Models.CarEvent.DiscountType;

namespace CarRentalServiceAPI.Services
{
    public class CarEventService: ICarEventService
    {
        private readonly ICarEventRepository _carEventRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;
        private readonly Dictionary<DiscountType, double> _discounts = new Dictionary<DiscountType, double>()
        {
            {DiscountType.NONE,0}, {DiscountType.KAT1,0.05}, {DiscountType.KAT2,0.1}, {DiscountType.KAT3,0.15}
        };

        public CarEventService(ICarEventRepository carEventRepository, ICarRepository carRepository, IUserRepository userRepository)
        {
            _carEventRepository = carEventRepository;
            _carRepository = carRepository;
            _userRepository = userRepository;
        }

        public async Task<CarReservationResponse> CreateReservation(CarEventDto carEventDto)
        {
            if (!await _userRepository.Exists(carEventDto.UserId ?? string.Empty))
                throw new BadHttpRequestException("There is no user with such Id");

            if (await IsReservationTimeAvailable(string.Empty,carEventDto.CarId??string.Empty,carEventDto.startDate??new DateTime(), carEventDto.finishDate ?? new DateTime()))
            {      

                var newCarReservation = new CarEvent()
                {
                    rentType = CarEvent.CarEventType.Reserved,
                    TransactionId = Guid.NewGuid().ToString(),
                    UserId = carEventDto.UserId,
                    CarId = carEventDto.CarId,
                    startDate = carEventDto.startDate ?? new DateTime(),
                    finishDate = carEventDto.finishDate ?? new DateTime(),
                    paidPrice = await GetPriceForCar(carEventDto.CarId),
                    Rate = 0,
                    DiscountPercentage = _discounts[DiscountType.NONE],                   
                };
                newCarReservation.paidPrice *= 1 - newCarReservation.DiscountPercentage;

                var newCarReservationResponse = new CarReservationResponse() { 
                    ReservationId = newCarReservation.TransactionId,
                    CarId = newCarReservation.CarId,
                    startDate = carEventDto.startDate,
                    finishDate=carEventDto.finishDate,
                    paidPrice=newCarReservation.paidPrice.Value,
                    Rate = newCarReservation.Rate.Value,
                    DiscountPercentage = newCarReservation.DiscountPercentage.Value,
                    eventType = CarEvent.CarEventType.Reserved,
                };

                await _carEventRepository.Create(newCarReservation);

                return newCarReservationResponse;
            }
            else
                throw new BadHttpRequestException("You can't make reservation in this date. Someone's already made a reservation.");
        }

        public async Task<CarReservationResponse> UpdateReservation(CarEventDto carEventDto)
        {
            var oldCarEventData = await _carEventRepository.Get(carEventDto.TransactionId??string.Empty);

            if(carEventDto.startDate != null || carEventDto.finishDate != null)
            {
                var newStartDate = carEventDto.startDate??oldCarEventData.startDate;
                var newFinishDate = carEventDto.finishDate??oldCarEventData.finishDate;
                if (!await IsReservationTimeAvailable(string.Empty,oldCarEventData.CarId??string.Empty, newStartDate??new DateTime(), newFinishDate??new DateTime()))
                    throw new BadHttpRequestException("there is a conflict with new reservation date. Try again!");
            }

            var response = await _carEventRepository.Update(carEventDto);

            var newCarReservationResponse = new CarReservationResponse()
            {
                startDate = carEventDto.startDate,
                finishDate = carEventDto.finishDate,
                Rate = carEventDto.Rate
            };

            return newCarReservationResponse;
        }

        public async Task<bool> DeleteReservation(string transactionId)
        {
            if (string.IsNullOrWhiteSpace(transactionId))
                throw new BadHttpRequestException("You new to input reservationId to delete record");

            await _carEventRepository.Delete(transactionId);

            return true;
        }

        public async Task<List<CarReservationResponse>> GetAllUserReservations(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new BadHttpRequestException("You need to input userId whose history you want to see");

            var userTransactions = await _carEventRepository.GetAllForUser(userId);

            return userTransactions;
        }

        private async Task<bool> IsReservationTimeAvailable(string transactionId,string carId,DateTime startDate, DateTime finishDate)
        {      
            var listOfRequestedCarEvents = await _carEventRepository.GetAllForRequestedCar(carId);

            listOfRequestedCarEvents = listOfRequestedCarEvents.Where(x => x.TransactionId != transactionId).ToList();

            var conflict = listOfRequestedCarEvents.Where(x => 
                        (startDate >= x.startDate && startDate <= x.finishDate)
                        || (finishDate >= x.startDate && finishDate <= x.finishDate)).Any();

            return !conflict;
        }

        private async Task<double> GetPriceForCar(string carId)
        {            
            var carPrice = await _carRepository.Get(carId);
            if (carPrice.Price != null)
                return carPrice.Price.Value;
            else
                throw new DriveNotFoundException("The Car has't requested price value");
        }
    }
}
