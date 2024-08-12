using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.DTO.GetDTO
{
    public record GetOrderDetailDTO
    {
        public int OrderId { get; set; }
        public List<Order>? Orders { get; set; }
        public int ProductId { get; set; }
        public List<Product>? Products { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
    }
}
