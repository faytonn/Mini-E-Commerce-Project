using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.DTO.GetDTO.AdminAccessedDTO
{
    public record GetOrderDetailDTOAdmin
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; } = null!;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
    }
}
