using Mini_E_Commerce_Project.Models.Common;

namespace Mini_E_Commerce_Project.Models;

public class User : BaseEntity
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Address { get; set; } = null!;
}
