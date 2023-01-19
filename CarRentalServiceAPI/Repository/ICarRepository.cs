using CarRentalServiceAPI.Data.Dto;

namespace CarRentalServiceAPI.Repository
{
    public interface ICarRepository
    {
        public Task<CarDto> Create(CarDto carDto, Base64FormattingOptions Image);
        public Task<CarDto> Update(CarDto carDto);
        public Task<bool> Delete(string carId);
        public Task<CarDto> Get(string carId);
    }
}
