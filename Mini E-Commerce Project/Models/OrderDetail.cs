using Mini_E_Commerce_Project.Models.Common;

namespace Mini_E_Commerce_Project.Models
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public Order? Orders { get; set; }
        public int ProductId { get; set; }
        public Product? Products { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
    }
}
