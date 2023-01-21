using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Data.Response;

namespace CarRentalServiceAPI.Services
{
    public interface ICarEventService
    {
        public Task<CarReservationResponse> CreateReservation(CarEventDto carEventDto);
    }
}
