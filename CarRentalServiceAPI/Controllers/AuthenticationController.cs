using CarRentalServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalServiceAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Login to user's Account and get session's token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> Login(string userName, string Password)
        {
            var response = await _authenticationService.LoginUser(userName,Password);

            return Ok(response);
        }

        /// <summary>
        /// Logout from user's account and delete session's token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Logout([FromQuery] string userId)
        {
            var response = await _authenticationService.LogoutUser(userId);

            return Ok(response);
        }
    }
}
