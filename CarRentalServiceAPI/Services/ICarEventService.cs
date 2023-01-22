using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Data.Response;

namespace CarRentalServiceAPI.Services
{
    public interface ICarEventService
    {
        public Task<CarReservationResponse> CreateReservation(CarEventDto carEventDto);
        public Task<CarReservationResponse> UpdateReservation(CarEventDto carEventDto);
        public Task<bool> DeleteReservation(string transactionId);
        public Task<List<CarReservationResponse>> GetAllUserReservations(string userId);
    }
}
