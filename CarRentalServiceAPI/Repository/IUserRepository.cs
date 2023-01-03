using CarRentalServiceAPI.Models;

namespace CarRentalServiceAPI.Repository
{
    public interface IUserRepository
    {
        public Task<List<User>> ReadAll();
        public Task<User> ReadSingleOne(string userId);
        public Task<bool> Update(User updatedUser);
        public Task<bool> Delete(string userId);
        public Task<User> Create(User newUser);
    }
}
