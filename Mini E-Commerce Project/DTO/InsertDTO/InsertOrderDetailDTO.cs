namespace Mini_E_Commerce_Project.DTO.InsertDTO
{
    public record InsertOrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
    }
}
