using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalServiceAPI.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }


        /// <summary>
        /// Creates new Car object
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(typeof(CarDto),200)]        
        public async Task<IActionResult> CreateCar([FromBody] CarDto car, [FromForm] IFormFile Image)
        {
            var response = await _carService.CreateCar(car,Image);

            return Ok(response);
        }
    }
}
