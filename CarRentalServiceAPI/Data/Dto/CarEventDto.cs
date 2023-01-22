namespace CarRentalServiceAPI.Data.Dto
{
    public class CarEventDto
    {
        public string? TransactionId { get; set; } = string.Empty;
        public string? UserId { get; set; } = string.Empty;
        public string? CarId { get; set; } = string.Empty;
        public int? Rate { get; set; } = 0;
        public DateTime? startDate { get; set; }
        public DateTime? finishDate { get; set; }       
        
    }
}
