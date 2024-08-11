using Mini_E_Commerce_Project.Enums;

namespace Mini_E_Commerce_Project.DTO
{
    public record OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public StatusEnum Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetailDTO>? OrderDetails { get; set; }
    }
}
