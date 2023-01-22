using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Data.Response;
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


        [HttpPut("reservation")]
        [ProducesResponseType(typeof(CarReservationResponse),200)]
        public async Task<IActionResult> EditReservation([FromForm] CarEventDto carEventDto)
        {
            var response = await _carEventService.UpdateReservation(carEventDto);

            return Ok(response);
        }
        
        [HttpDelete("reservation")]
        [ProducesResponseType(typeof(bool),200)]
        public async Task<IActionResult> DeleteReservation([FromQuery] string reservationId)
        {
            var response = await _carEventService.DeleteReservation(reservationId);

            return Ok(response);
        }

        [HttpGet("reservations")]
        [ProducesResponseType(typeof(List<CarReservationResponse>),200)]
        public async Task<IActionResult> GetAllUserReservations(string userId)
        {
            var response = await _carEventService.GetAllUserReservations(userId);

            return Ok(response);
        }

    }
}
