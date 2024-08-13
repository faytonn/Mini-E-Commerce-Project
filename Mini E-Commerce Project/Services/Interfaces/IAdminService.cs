using Mini_E_Commerce_Project.DTO.GetDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;

namespace Mini_E_Commerce_Project.Services.Interfaces
{
    public interface IAdminService 
    {
        Task<List<GetUserDTO>> GetAllUsersAsync();
        Task<GetUserDTO> GetUserByIdAsync(int id);
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(InsertUserDTO userDto);
    }
}
