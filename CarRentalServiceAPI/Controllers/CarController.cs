using CarRentalServiceAPI.Data.Dto;
using CarRentalServiceAPI.Data.Response;
using CarRentalServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalServiceAPI.Controllers
{
    [Route("api/")]
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
        [HttpPost("car")]
        [ProducesResponseType(typeof(CarDto),200)]        
        public async Task<IActionResult> CreateCar([FromForm] CarDto car)
        {
            var response = await _carService.CreateCar(car);

            return Ok(response);
        }

        /// <summary>
        /// Get details of specified car
        /// </summary>
        [HttpGet("car")]
        [ProducesResponseType(typeof(CarResponse),200)]
        public async Task<IActionResult> GetCarDetails([FromQuery] string carId)
        {
            var response = await _carService.GetCar(carId);

            return Ok(response);
        }

        /// <summary>
        /// Deletes record from database by carId
        /// </summary>
        [HttpDelete("car")]
        [ProducesResponseType(typeof(bool),200)]
        public async Task<IActionResult> DeleteCar([FromQuery] string carId)
        {
            var response = await _carService.DeleteCar(carId);

            return Ok(response);
        }

        /// <summary>
        /// Updates car record in database
        /// </summary>
        [HttpPut("car")]
        [ProducesResponseType(typeof(CarDto),200)]
        public async Task<IActionResult> UpdateCarDetails([FromForm] CarDto newCarData)
        {
            var response = await _carService.UpdateCarRecord(newCarData);

            return Ok(response);
        }

        /// <summary>
        /// Get filtered list of cars 
        /// </summary>
        [HttpGet("cars")]
        [ProducesResponseType(typeof(List<CarResponse>),200)]
        public async Task<IActionResult> GetCarsList([FromQuery] Filter filter)
        {
            var response = await _carService.GetListOfCars(filter);

            return Ok(response);
        }
    }
}
