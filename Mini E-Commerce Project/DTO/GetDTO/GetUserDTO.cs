namespace Mini_E_Commerce_Project.DTO.GetDTO
{
    public record GetUserDTO
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
