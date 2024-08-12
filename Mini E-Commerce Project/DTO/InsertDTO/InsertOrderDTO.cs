using Mini_E_Commerce_Project.Enums;

namespace Mini_E_Commerce_Project.DTO.InsertDTO
{
    public record InsertOrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public StatusEnum Status { get; set; }
    }
}
