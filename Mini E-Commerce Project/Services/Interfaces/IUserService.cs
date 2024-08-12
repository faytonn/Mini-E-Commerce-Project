using Mini_E_Commerce_Project.DTO.DTOModels;

namespace Mini_E_Commerce_Project.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task CreateUserAsync(UserDTO userDto);
        Task UpdateUserAsync(UserDTO userDto);
        Task DeleteUserAsync(int id);
    }
}
