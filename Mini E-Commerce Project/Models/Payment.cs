using Mini_E_Commerce_Project.Models.Common;

namespace Mini_E_Commerce_Project.Models
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
