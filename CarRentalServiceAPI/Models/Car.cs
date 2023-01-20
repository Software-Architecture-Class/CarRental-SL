using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Models
{
    [PrimaryKey(nameof(CarId))]
    public class Car
    {
        public enum CarCategory { SUV }
        public enum GearboxType { Manual, Mechanic }
        public string? CarId { get; set; }
        public string? Brand { get; set; } = string.Empty;
        public string? Model { get; set; } = string.Empty;
        public int? Power { get; set; } = 0;
        public int? Acceleration { get; set; } = 0;//przyspieszenie
        public GearboxType? gearboxType { get; set; }//rodzaj skrzyni biegów
        public int? Drive { get; set; } = 0; //napęd
        public CarCategory? carCategory { get; set; }
        public string? Description { get; set; } = string.Empty;
        public double? Price { get; set; } = 0;//for one day
        public int? peopleCapacity { get; set; } = 0;//how many people can contain car
        public string? Image { get; set; } = string.Empty;
        public string? ImageTitle { get; set; } = string.Empty;
        public int? ReleaseDate { get; set; } = 0;
        public int? Popularity { get; set; } = 0; //how many times was it rent
        public DateTime LastTimeModified { get; set; }
    }
}
