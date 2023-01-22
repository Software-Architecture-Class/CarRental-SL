using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Models
{
    [PrimaryKey(nameof(CarId))]
    public class Car
    {
        public enum CarCategory { SUV, Coupe, Sedan, Jeep, Hatchback, Kombi, Sportowe, Limuzyna }
        public enum GearboxType { Manual, Automatic }
        public enum DriveType { RWD, AWD, FWD}
        public string? CarId { get; set; }
        public string? Brand { get; set; } = string.Empty;
        public string? Model { get; set; } = string.Empty;
        public int? Power { get; set; } = 0;
        public double? Acceleration { get; set; } = 0;//przyspieszenie
        public double? TimeFrom0To100 { get; set; } = 0;
        public double? EngineCapacity { get; set; } = 0;//pojemność silnika        
        public GearboxType? gearboxType { get; set; }//rodzaj skrzyni biegów
        public DriveType? Drive { get; set; } //napęd
        public CarCategory? carCategory { get; set; }
        public string? Description { get; set; } = string.Empty;
        public double? Price { get; set; } = 0;//for one day
        public int? peopleCapacity { get; set; } = 0;//how many people can contain car
        public string? Image { get; set; } = string.Empty;        
        public int? ReleaseDate { get; set; } = 0;
        public int? Popularity { get; set; } = 0; //how many times was it rent
        public DateTime LastTimeModified { get; set; }
    }
}
