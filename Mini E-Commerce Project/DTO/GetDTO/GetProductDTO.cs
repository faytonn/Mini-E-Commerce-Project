namespace Mini_E_Commerce_Project.DTO.GetDTO
{
    public record GetProductDTO
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = null!;
        
    }
}
