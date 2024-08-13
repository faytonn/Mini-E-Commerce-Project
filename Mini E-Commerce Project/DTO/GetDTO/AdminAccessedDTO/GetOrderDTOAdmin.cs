using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.DTO.GetDTO.AdminAccessedDTO
{
    public record GetOrderDTOAdmin
    {
        public int UserId { get; set; }
        public string UsersName { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public StatusEnum Status { get; set; }
        public List<GetOrderDetailDTOAdmin>? OrderDetails { get; set; }

    }
}
