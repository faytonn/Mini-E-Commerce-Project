namespace Mini_E_Commerce_Project.DTO.GetDTO.AdminAccessedDTO
{
    public record GetProductDTOAdmin
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
