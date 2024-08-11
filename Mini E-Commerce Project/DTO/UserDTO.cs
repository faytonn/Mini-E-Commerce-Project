﻿namespace Mini_E_Commerce_Project.DTO
{
    public record UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
