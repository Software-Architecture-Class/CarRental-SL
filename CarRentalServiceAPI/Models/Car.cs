using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Models
{
    [PrimaryKey(nameof(CarId))]
    public class Car
    {
        public enum CarCategory { SUV }
        public enum GearboxType { Manual, Mechanic }
        public Guid CarId { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Power { get; set; }
        public int Acceleration { get; set; }//przyspieszenie
        public GearboxType gearboxType { get; set; }//rodzaj skrzyni biegów
        public int Drive { get; set; } //napęd
        public CarCategory carCategory { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }//for one day
        public int peopleCapacity { get; set; }//how many people can contain car
        public Base64FormattingOptions Image { get; set; }
        public int ReleaseDate { get; set; }
        public int Popularity { get; set; } //how many times was it rent
        public DateTime LastTimeModified { get; set; }
    }
}
