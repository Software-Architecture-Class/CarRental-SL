using static CarRentalServiceAPI.Models.CarEvent;

namespace CarRentalServiceAPI.Data.Response
{
    public class CarReservationResponse
    {          
        public string ReservationId { get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }
        public double paidPrice { get; set; }
        public int Rate { get; set; }
        public double DiscountPercentage { get; set; }
        public CarEventType eventType { get; set; }
    }
}
