using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Models
{
    [PrimaryKey(nameof(UserId))]    
    public class User
    {
        public enum UserType { Client, Admin }
        public string? UserId { get; set; } = string.Empty;        
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? EmailAddress { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? CardNumber { get; set; } = string.Empty;
        public UserType? userType { get; set; }
        public DateTime? LastTimeModified { get; set; }
    }
}
