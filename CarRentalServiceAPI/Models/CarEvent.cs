using Microsoft.EntityFrameworkCore;

namespace CarRentalServiceAPI.Models
{
    [PrimaryKey(nameof(TransactionId))]
    public class CarEvent
    {
        public enum CarEventType {OnLoan, Reserved, InRepair, InCleaning, NotAvailable}
        public enum DiscountType { KAT1, KAT2, KAT3, NONE }// KAT1 - 5%, KAT2 - 10%, KAT3 - 15%, NONE - 0%
        public Guid TransactionId { get; set; }
        public Guid UserId { get; set; }
        public Guid CarId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }
        public double paidPrice { get; set; }
        public int Rate { get; set; } //0-10
        public double DiscountPercentage { get; set; }
        public DiscountType Discount { get; set; }
        public CarEventType rentType { get; set; }
    }
}
