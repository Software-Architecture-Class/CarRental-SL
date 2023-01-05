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
                        
            if(!string.IsNullOrEmpty(updatedUser.FirstName)) user.FirstName = updatedUser.FirstName;
            if(!string.IsNullOrEmpty(updatedUser.LastName)) user.LastName = updatedUser.LastName;
            if(!string.IsNullOrEmpty(updatedUser.Address)) user.Address = updatedUser.Address;
            if(!string.IsNullOrEmpty(updatedUser.CardNumber)) user.CardNumber = updatedUser.CardNumber;
            user.LastTimeModified = DateTime.Now;

            _context.SaveChanges();

            return true;
        }

        public async Task<bool> Exists(string userId)
        {
            return await _context.Users.FindAsync(userId) != null;
        }
    }
}
