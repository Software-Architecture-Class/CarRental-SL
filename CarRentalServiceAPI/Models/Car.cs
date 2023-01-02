namespace CarRentalServiceAPI.Models
{
    public enum CarCategory { }
    public enum GearboxType { }
    public class Car
    {
        public Guid CarId { get; set; }
        public string? Brand { get; set; }
        public int Power { get; set; }
        public int Acceleration { get; set; }//przyspieszenie
        public GearboxType GearboxType { get; set; }//rodzaj skrzyni biegów
        public int Drive { get; set; } //napęd
        public CarCategory CarCategory { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }//for one day
        public int peopleCapacity { get; set; }//how many people can contain car
        public Base64FormattingOptions Image { get; set; }
        public int ReleaseDate { get; set; }
        public int Popularity { get; set; } //how many times was it rent

    }
}
