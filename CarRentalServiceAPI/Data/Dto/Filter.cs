namespace CarRentalServiceAPI.Data.Dto
{    
    public class Filter
    {
        public enum SortingOptions
        {
            PriceHighToLow,
            PriceLowToHigh,
            Brand,
            Acceleration,
            ReleaseDate,
            Popularity
        }
        public SortingOptions Option { get; set; }
    }
}
