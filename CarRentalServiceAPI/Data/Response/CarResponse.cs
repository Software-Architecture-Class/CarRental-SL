using static CarRentalServiceAPI.Models.Car;

namespace CarRentalServiceAPI.Data.Response
{
    public class CarResponse
    {
        public string? CarId { get; set; }
        public string? Brand { get; set; } = string.Empty;
        public string? Model { get; set; } = string.Empty;
        public int? Power { get; set; }
        public int? Acceleration { get; set; }//przyspieszenie
        public GearboxType? gearboxType { get; set; }//rodzaj skrzyni biegów
        public int? Drive { get; set; } //napęd
        public CarCategory? carCategory { get; set; }
        public string? Description { get; set; } = string.Empty;
        public double? Price { get; set; }//for one day
        public int? peopleCapacity { get; set; }//how many people can contain car
        public string? Image { get; set; } = string.Empty;        
        public int? ReleaseDate { get; set; }
        public int? Popularity { get; set; } //how many times was it rent        
    }
}
