using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalServiceAPI.Controllers
{
    [Route("api/car-events/")]
    [ApiController]
    public class CarEventController : ControllerBase
    {
        private readonly ICarEventService _carEventService;

        public CarEventController(ICarEventService carEventService)
        {
            _carEventService = carEventService;
        }

        [HttpPost("reservation")]
        [ProducesResponseType(typeof(string),200)]
        public async Task<IActionResult> CreateCarReservation([FromForm] CarEventDto carEventDto)
        {
            var response = await _carEventService.CreateReservation(carEventDto);            

            return Ok(response);
        }

    }
}
