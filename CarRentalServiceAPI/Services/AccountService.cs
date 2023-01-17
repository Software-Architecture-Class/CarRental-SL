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
               
        public async Task<User> UpdateAccount(AccountDto data)
        {
            User newUser = new User();
            AuthenticationCredentials newAuthenticationCredentials = new AuthenticationCredentials();
            if(!string.IsNullOrEmpty(data.UserName)) newAuthenticationCredentials.UserName = data.UserName;
            if(!string.IsNullOrEmpty(data.Password))
            {
                CreatePasswordHash(data.Password, out byte[] passwordHash, out byte[] passwordSalt);
                newAuthenticationCredentials.PasswordHash = passwordHash;
                newAuthenticationCredentials.PasswordSalt = passwordSalt;
            }
            if(!string.IsNullOrEmpty(data.FirstName)) newUser.FirstName = data.FirstName;
            if(!string.IsNullOrEmpty(data.LastName)) newUser.LastName = data.LastName;
            if(!string.IsNullOrEmpty(data.Address)) newUser.Address = data.Address;
            if(!string.IsNullOrEmpty(data.CardNumber)) newUser.CardNumber = data.CardNumber;

            newUser.UserId = data.UserId;
            newAuthenticationCredentials.UserId = data.UserId;

            await _userRepository.Update(newUser);
            await _authenticationRepository.Update(newAuthenticationCredentials);

            return newUser;
        }

        public async Task<AccountDto> GetUserData(string userId)
        {
            var userGenerallData = await _userRepository.ReadSingleOne(userId);
            var userName = (await _authenticationRepository.ReadSingleOne(userId)).UserName;

            var accountDto = new AccountDto()
            {
                UserName = userName,
                UserId = userId,
                FirstName = userGenerallData.FirstName,
                LastName = userGenerallData.LastName,
                Address = userGenerallData.Address,
                CardNumber = userGenerallData.CardNumber,
            };

            return accountDto;
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
