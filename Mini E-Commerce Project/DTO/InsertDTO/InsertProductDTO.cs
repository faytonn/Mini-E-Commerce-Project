namespace Mini_E_Commerce_Project.DTO.InsertDTO
{
    public record InsertProductDTO
    {
            public string Name { get; set; } = null!;
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public string Description { get; set; } = null!;
        
    }

}

