using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static CarRentalServiceAPI.Models.Car;

namespace CarRentalServiceAPI.Data.Dto
{
    public class CarDto
    {
        public string? CarId { get; set; } = string.Empty;
        public string? Brand { get; set; } = string.Empty;
        public string? Model { get; set; } = string.Empty;
        public int? Power { get; set; }
        public int? Acceleration { get; set; }                
        public GearboxType? gearboxType { get; set; }
        public int? Drive { get; set; }
        public CarCategory? carCategory { get; set; }
        public string? Description { get; set; } = string.Empty; 
        public double? Price { get; set; }
        public int? PeopleCapacity { get; set; }       
        public int? ReleaseDate { get; set; }  
        public IFormFile? Image { get; set; }
    }
}
