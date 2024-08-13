namespace Mini_E_Commerce_Project.DTO.ServiceDTO
{
    public record CreateOrderDetailDTO
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
    }
}
