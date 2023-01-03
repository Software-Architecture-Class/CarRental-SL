namespace CarRentalServiceAPI.Services
{
    public interface IAuthenticationService
    {        
        public Task LogInUser();
        public Task LogOutUser();        
    }
}
