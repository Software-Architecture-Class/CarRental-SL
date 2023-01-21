using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Models
{
    [PrimaryKey(nameof(TransactionId))]
    public class CarEvent
    {
        public enum CarEventType {OnLoan, Reserved, InRepair, InCleaning, NotAvailable}
        public enum DiscountType { KAT1, KAT2, KAT3, NONE }// KAT1 - 5%, KAT2 - 10%, KAT3 - 15%, NONE - 0%
        public string? TransactionId { get; set; }
        public string? UserId { get; set; }
        public string? CarId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }
        public double? paidPrice { get; set; }
        public int? Rate { get; set; } //0-10
        public double? DiscountPercentage { get; set; }
        public DiscountType? Discount { get; set; }
        public CarEventType? rentType { get; set; }
        public DateTime LastTimeModified { get; set; }
    }
}
