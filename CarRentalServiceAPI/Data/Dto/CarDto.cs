using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static CarRentalServiceAPI.Models.Car;
using DriveType = CarRentalServiceAPI.Models.Car.DriveType;

namespace CarRentalServiceAPI.Data.Dto
{
    public class CarDto
    {
        public string? CarId { get; set; } = string.Empty;
        public string? Brand { get; set; } = string.Empty;
        public string? Model { get; set; } = string.Empty;
        public int? Power { get; set; }
        public double? Acceleration { get; set; }                
        public GearboxType? gearboxType { get; set; }
        public DriveType? Drive { get; set; }
        public CarCategory? carCategory { get; set; }
        public string? Description { get; set; } = string.Empty;
        public double? EngineCapacity { get; set; } = 0;
        public double? Price { get; set; }
        public int? PeopleCapacity { get; set; }       
        public int? ReleaseDate { get; set; }  
        public IFormFile? Image { get; set; }
    }
}
