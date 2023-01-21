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
        private readonly Dictionary<DiscountType, double> _discounts = new Dictionary<DiscountType, double>()
        {
            {DiscountType.NONE,0}, {DiscountType.KAT1,0.05}, {DiscountType.KAT2,0.1}, {DiscountType.KAT3,0.15}
        };

        public CarEventService(ICarEventRepository carEventRepository, ICarRepository carRepository)
        {
            _carEventRepository = carEventRepository;
            _carRepository = carRepository;
        }

        public async Task<CarReservationResponse> CreateReservation(CarEventDto carEventDto)
        {
            if (await IsReservationTimeAvailable(carEventDto.CarId,carEventDto.startDate, carEventDto.finishDate))
            {
                //musi sprawdzić czy istnieje użytkownik o danym ID

                var newCarReservation = new CarEvent()
                {
                    rentType = CarEvent.CarEventType.Reserved,
                    TransactionId = Guid.NewGuid().ToString(),
                    UserId = carEventDto.UserId,
                    CarId = carEventDto.CarId,
                    startDate = carEventDto.startDate,
                    finishDate = carEventDto.finishDate,
                    paidPrice = await GetPriceForCar(carEventDto.CarId),
                    Rate = 0,
                    DiscountPercentage = _discounts[DiscountType.NONE],                   
                };
                newCarReservation.paidPrice *= 1 - newCarReservation.DiscountPercentage;

                var newCarReservationResponse = new CarReservationResponse() { 
                    ReservationId = newCarReservation.TransactionId,
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
        private async Task<bool> IsReservationTimeAvailable(string carId,DateTime startDate, DateTime finishDate)
        {
            var listOfRequestedCarEvents = await _carEventRepository.GetAllForRequestedCar(carId);

            var conflict = listOfRequestedCarEvents.Where(x => 
                        (startDate > x.startDate && startDate < x.finishDate)
                        || (finishDate > x.startDate && finishDate < x.finishDate)).Any();

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
