using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Data.Response;
using CarRentalServiceAPI.Models;

namespace CarRentalServiceAPI.Repository
{
    public interface ICarEventRepository
    {
        public Task<bool> Create(CarEvent carEvent);
        public Task<CarEventDto> Update(CarEventDto carEvent);
        public Task<List<CarEvent>> GetAllForRequestedCar(string carId);
        public Task<CarReservationResponse> Get(string transactionId);
        public Task<bool> Delete(string transactionId);
        public Task<List<CarReservationResponse>> GetAllForUser(string userId);
    }
}
