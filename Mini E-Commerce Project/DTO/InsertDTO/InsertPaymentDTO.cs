namespace Mini_E_Commerce_Project.DTO.InsertDTO
{
    public record InsertPaymentDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
