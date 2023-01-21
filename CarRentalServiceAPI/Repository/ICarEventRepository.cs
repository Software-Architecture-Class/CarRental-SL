using CarRentalServiceAPI.Models;

namespace CarRentalServiceAPI.Repository
{
    public interface ICarEventRepository
    {
        public Task<bool> Create(CarEvent carEvent);
        public Task<List<CarEvent>> GetAllForRequestedCar(string carId);
    }
}
