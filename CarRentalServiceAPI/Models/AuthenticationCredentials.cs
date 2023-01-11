using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Models
{
    [PrimaryKey(nameof(UserId))]
    public class AuthenticationCredentials
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = { };
        public byte[] PasswordSalt { get; set; } = { };
        public string Token { get; set; } = string.Empty;
    }
}
