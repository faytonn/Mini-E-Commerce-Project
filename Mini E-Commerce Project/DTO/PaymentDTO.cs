namespace Mini_E_Commerce_Project.DTO
{
    public record PaymentDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
