namespace Mini_E_Commerce_Project.DTO.GetDTO.AdminAccessedDTO
{
    public record GetUserDTOAdmin
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool isAdmin { get; set; }
    }
}
