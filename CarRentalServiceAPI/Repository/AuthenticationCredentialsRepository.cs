using CarRentalServiceAPI.Data;
using CarRentalServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Repository
{
    public class AuthenticationCredentialsRepository : IAuthenticationCredentialsRepository
    {
        private readonly DbConnectionContext _context;

        public AuthenticationCredentialsRepository(DbConnectionContext context)
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

        public async Task<AuthenticationCredentials> ReadSingleOne(string userName)
        {
            var userCredentials = await _context.AuthenticationCredentials.FirstOrDefaultAsync(x => x.UserName == userName);

            if (userCredentials == null)
                throw new BadHttpRequestException("There is no user with such userId");
            else
                return userCredentials;
        }

        public async Task<bool> Update(AuthenticationCredentials updatedcCredentials)
        {
            var oldCredentials = await _context.AuthenticationCredentials.FindAsync(updatedcCredentials.UserId);
            if (oldCredentials == null)
                throw new BadHttpRequestException("User with such userName already exists");

            if(!string.IsNullOrEmpty(updatedcCredentials.UserName)) oldCredentials.UserName = updatedcCredentials.UserName;
            if (updatedcCredentials.PasswordHash != new byte[] {}) oldCredentials.PasswordHash = updatedcCredentials.PasswordHash;
            if(updatedcCredentials.PasswordSalt != new byte[] {}) oldCredentials.PasswordSalt = updatedcCredentials.PasswordSalt;
            if(!string.IsNullOrEmpty(updatedcCredentials.Token)) oldCredentials.Token = updatedcCredentials.Token;

            _context.SaveChanges();

            return true;
        }

        public async Task<bool> ChangeToken(string userId, string token)
        {
            var oldCredentials = await _context.AuthenticationCredentials.FindAsync(userId);
            if (oldCredentials == null)
                throw new BadHttpRequestException("User with such userName already exists");

            oldCredentials.Token = token;
            _context.SaveChanges();

            return true;
        }
    }
}
