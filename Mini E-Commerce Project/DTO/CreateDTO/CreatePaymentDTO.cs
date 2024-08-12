namespace Mini_E_Commerce_Project.DTO.ServiceDTO
{
    public record CreatePaymentDTO
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }

    }
}
