using CarRentalServiceAPI.Models;

namespace CarRentalServiceAPI.Repository
{
    public interface IAuthenticationCredentialsRepository
    {
        public Task<AuthenticationCredentials> ReadSingleOne(string UserName);
        public Task<AuthenticationCredentials> Create(AuthenticationCredentials credentials);        
        public Task<bool> Delete(string userId);
        public Task<bool> Update(AuthenticationCredentials credentials);
        public Task<bool> ChangeToken(string userId, string token);

        public Task<AuthenticationCredentials> GetDataByUserName(string userName);
    }
}
