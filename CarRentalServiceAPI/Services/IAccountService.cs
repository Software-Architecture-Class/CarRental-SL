using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Models;

namespace CarRentalServiceAPI.Services
{
    public interface IAccountService
    {
        public Task<User> RegisterAccount(AccountDto request);
        public Task<bool> DeleteAccount(string UserId);
    }
}
