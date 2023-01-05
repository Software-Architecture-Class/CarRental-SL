namespace CarRentalServiceAPI.Services
{
    public interface IAuthenticationService
    {        
        public Task<string> LoginUser(string userName, string Password);
        public Task<bool> LogoutUser(string userId);        
    }
}
