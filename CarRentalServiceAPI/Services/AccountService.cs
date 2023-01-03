using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Models;
using System.Security.Cryptography;

namespace CarRentalServiceAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly User _user = new User();
        private readonly AuthenticationCredentials _authenticationCredentials = new AuthenticationCredentials();
        
        public async Task<User> RegisterAccount(AccountDto request)
        {
            if (!IsUsernameAvailable(request.UserName)) throw new BadHttpRequestException("Given Username is already used by another User");
            InsertData(request);
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            _authenticationCredentials.PasswordHash = passwordHash;
            _authenticationCredentials.PasswordSalt = passwordSalt;

            return _user;
        }

        public async Task<bool> DeleteAccount(string userName)
        {
            if (!IsUsernameAvailable(userName)) throw new BadHttpRequestException("There is no user with such username");

            return true;
        }

        private bool IsUsernameAvailable(string userName)
        {
            return true;
        }

        private void InsertData(AccountDto data)
        {
            _user.UserId = Guid.NewGuid().ToString();
            _user.FirstName = data.FirstName;
            _user.LastName = data.LastName;
            _user.Address = data.Address;
            _user.CardNumber = data.CardNumber;
            _user.userType = User.UserType.Client;
            _user.AccountCreationDate = DateTime.Now;
            _authenticationCredentials.UserId = _user.UserId;
            _authenticationCredentials.UserName = data.UserName;            
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
