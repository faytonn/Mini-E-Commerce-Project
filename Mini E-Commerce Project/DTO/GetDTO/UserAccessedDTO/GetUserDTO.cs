namespace Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO
{
    public record GetUserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool isAdmin { get; set; }
    }
}
