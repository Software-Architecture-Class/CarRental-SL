using CarRentalServiceAPI.Data;
using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Models;
using CarRentalServiceAPI.Repository;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CarRentalServiceAPI.Services
{
    public class AccountService : IAccountService
    {        
        private readonly IAuthenticationCredentialsRepository _authenticationRepository;
        private readonly IUserRepository _userRepository;

        public AccountService(IAuthenticationCredentialsRepository authenticationRepository, IUserRepository userRepository)
        {
            _authenticationRepository = authenticationRepository;
            _userRepository = userRepository;
        }

        public async Task<User> RegisterAccount(AccountDto request)
        {            
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newAuthenticationCredentials = new AuthenticationCredentials()
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            await _authenticationRepository.Create(newAuthenticationCredentials);

            var newUser = new User()
            {
                userType = User.UserType.Client,
                UserId = newAuthenticationCredentials.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                CardNumber = request.CardNumber,
                LastTimeModified = DateTime.Now
            };
            await _userRepository.Create(newUser);

            return newUser;
        }

        public async Task<bool> DeleteAccount(string userId)
        {
            await _authenticationRepository.Delete(userId);
            await _userRepository.Delete(userId);

            return true;
        }
            
        //do poprawy 
        private async Task<User> UpdateAccount(AccountDto data)
        {
            var user = new User()
            {
                UserId = data.UserId,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Address = data.Address,
                CardNumber = data.CardNumber,
                LastTimeModified = DateTime.Now
            };
            await _userRepository.Update(user);

            CreatePasswordHash(data.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var authenticationCredentials = new AuthenticationCredentials()
            {
                UserName = data.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };            

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
