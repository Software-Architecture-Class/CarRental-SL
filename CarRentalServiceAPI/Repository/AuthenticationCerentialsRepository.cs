using CarRentalServiceAPI.Data;
using CarRentalServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Repository
{
    public class AuthenticationCerentialsRepository : IAuthenticationCredentialsRepository
    {
        private readonly DbConnectionContext _context;

        public AuthenticationCerentialsRepository(DbConnectionContext context)
        {
            _context = context;
        }

        public async Task<AuthenticationCredentials> Create(AuthenticationCredentials credentials)
        {
            if (_context.AuthenticationCredentials.Find(credentials.UserId) != null ||
                _context.AuthenticationCredentials.Find(credentials.UserName) != null)
                throw new BadHttpRequestException("User with such userName or userId already exists");

            await _context.AuthenticationCredentials.AddAsync(credentials);   
            await _context.SaveChangesAsync();
            
            return credentials;
        }

        public async Task<bool> Delete(string userId)
        {
            var foundUser = await _context.AuthenticationCredentials.FindAsync(userId);
            if (foundUser != null)
            {
                _context.AuthenticationCredentials.Remove(foundUser);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public async Task<AuthenticationCredentials> ReadSingleOne(string userId)
        {
            var userCredentials = await _context.AuthenticationCredentials.FindAsync(userId);

            if (userCredentials == null)
                throw new BadHttpRequestException("There is no user with such userId");
            else
                return userCredentials;
        }

        //public async Task<bool> Update()
        //{

        //}
    }
}
