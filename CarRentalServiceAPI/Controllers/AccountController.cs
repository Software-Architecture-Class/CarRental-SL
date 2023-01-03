using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalServiceAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {        
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {            
            _accountService = accountService;
        }

        [HttpPost()]        
        public async Task<IActionResult> Register([FromBody] AccountDto request)
        {
            var response = await _accountService.RegisterAccount(request);
            
            return Ok(response);
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteAccount([FromQuery] string UserId)
        {
            var response = await _accountService.DeleteAccount(UserId);

            return Ok(response);
        }
    }
}
