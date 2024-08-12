namespace Mini_E_Commerce_Project.DTO.GetDTO
{
    public record GetPaymentDTO
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
