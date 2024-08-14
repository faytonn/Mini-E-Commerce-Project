namespace Mini_E_Commerce_Project.DTO.MiscellaneousDTO
{
    public record RefundDTO
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
