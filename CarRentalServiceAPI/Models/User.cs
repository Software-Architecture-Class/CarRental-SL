namespace CarRentalServiceAPI.Models
{
    public enum UserType { Client, Admin }
    public class User
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? CardNumber { get; set; }
        public UserType UserType { get; set; }
        public DateTime? AccountCreationDate { get; set; }
    }
}
