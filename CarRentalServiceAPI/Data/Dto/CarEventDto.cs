namespace CarRentalServiceAPI.Data.Dto
{
    public class CarEventDto
    {
        public string UserId { get; set; } = string.Empty;
        public string CarId { get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }       
        
    }
}
