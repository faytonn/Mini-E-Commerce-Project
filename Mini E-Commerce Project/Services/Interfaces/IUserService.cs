using Mini_E_Commerce_Project.DTO.GetDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(CreateUserDTO registerUser);
        Task LoginAsync (User loginUser);
        Task UpdateUserAsync(InsertUserDTO userDto);
    }
}
