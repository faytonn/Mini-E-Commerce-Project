using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.DTO.GetDTO.AdminAccessedDTO
{
    public record GetOrderDTOAdmin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User Users { get; set; } = null!;
        public string UsersName { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public StatusEnum Status { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new();

    }
}
