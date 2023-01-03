using CarRentalServiceAPI.Models;

namespace CarRentalServiceAPI.Repository
{
    public interface IAuthenticationCredentialsRepository
    {
        public Task<AuthenticationCredentials> ReadSingleOne(string UserId);
        public Task<AuthenticationCredentials> Create(AuthenticationCredentials credentials);        
        public Task<bool> Delete(string userId);
    }
}
