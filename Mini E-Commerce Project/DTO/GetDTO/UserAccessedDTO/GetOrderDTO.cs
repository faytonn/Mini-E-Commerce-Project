using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Enums;

namespace Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO
{
    public record GetOrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CreateOrderDetailDTO>? OrderDetails { get; set; }
        public decimal TotalAmount { get; set; }
        public StatusEnum Status { get; set; }

    }
}
