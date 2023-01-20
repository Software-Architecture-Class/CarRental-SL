using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Models;
using CarRentalServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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

        /// <summary>
        /// Register new user's account.
        /// </summary>        
        [HttpPost()]
        [ProducesResponseType(typeof(User), 200)]
        [Description("Create new user's account.")]
        public async Task<IActionResult> Register([FromForm] AccountDto request)
        {
            var response = await _accountService.RegisterAccount(request);
            
            return Ok(response);
        }


        /// <summary>
        /// Delete user's account.
        /// </summary>
        [HttpDelete()]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteAccount([FromQuery] string UserId)
        {
            var response = await _accountService.DeleteAccount(UserId);

            return Ok(response);
        }

        /// <summary>
        /// Update user's account data
        /// </summary>
        [HttpPut()]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> UpdateAccount([FromForm] AccountDto request)
        {
            var response = await _accountService.UpdateAccount(request);

            return Ok(response);
        }


        /// <summary>
        /// Returnns Generall Data of User's Account
        /// </summary>        
        [HttpGet()]
        [ProducesResponseType(typeof(AccountDto),200)]
        public async Task<IActionResult> GetAccount([FromQuery] string userId)
        {
            var accountData = await _accountService.GetUserData(userId);

            return Ok(accountData);
        }
    }
}
