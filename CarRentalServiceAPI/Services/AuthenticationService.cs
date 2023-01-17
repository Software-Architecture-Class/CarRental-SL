using CarRentalServiceAPI.Models;
using CarRentalServiceAPI.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CarRentalServiceAPI.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationCredentialsRepository _authenticationRepository;

        public AuthenticationService(IConfiguration configuration,IAuthenticationCredentialsRepository authenticationRepository)
        {
            _configuration = configuration;
            _authenticationRepository = authenticationRepository;
        }

        public async Task<string> LoginUser(string userName, string Password)
        {
            var credentials = await _authenticationRepository.GetDataByUserName(userName);            
                
            if(!VerifyPasswordHash(Password, credentials.PasswordHash, credentials.PasswordSalt))
            {
                return "Your userName or Password are incorrect. Try again.";
            }

            var token = CreateToken(credentials);
            credentials.Token = token;
            
            await _authenticationRepository.ChangeToken(credentials.UserId, token);          

            return token;

        }

        public async Task<bool> LogoutUser(string userId)
        {            
            await _authenticationRepository.ChangeToken(userId,string.Empty);

            return true;
        }
        
        private string CreateToken(AuthenticationCredentials authenticationData)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,authenticationData.UserId)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("Authentication:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );  
            
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
