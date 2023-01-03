using CarRentalServiceAPI.Data;
using CarRentalServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionContext _context;

        public UserRepository(DbConnectionContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User newUser)
        {
            if (_context.Users.Find(newUser.UserId) != null)
                throw new BadHttpRequestException("User with such userId already exists");

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<bool> Delete(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new BadHttpRequestException("User with such userId doesn't exists");

            _context.Remove(user);
            _context.SaveChanges();

            return true;
        }

        public async Task<List<User>> ReadAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> ReadSingleOne(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new BadHttpRequestException("User with such userId doesn't exist");

            return user;
        }

        public async Task<bool> Update(User updatedUser)
        {
            var user = await _context.Users.FindAsync(updatedUser.UserId);
            if (user == null)
                throw new BadHttpRequestException("User with such userId doesn't exist");
                        
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Address = updatedUser.Address;
            user.CardNumber = updatedUser.CardNumber;
            user.LastTimeModified = updatedUser.LastTimeModified;
            _context.SaveChanges();

            return true;
        }
    }
}
