using Mini_E_Commerce_Project.DTO.GetDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Repositories.Implementations;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using Mini_E_Commerce_Project.Services.Interfaces;

namespace Mini_E_Commerce_Project.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;

        public AdminService()
        {
            _userRepository = new UserRepository();
        }
        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetSingleAsync(u => u.Id == id);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();

        }

        public async Task<List<GetUserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var userDTOs = new List<GetUserDTO>();

            foreach(var user in users)
            {
                GetUserDTO userDTO = new GetUserDTO()
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Address = user.Address,

                };
            }
        }

        public Task<GetUserDTO> GetUserByIdAsync(int id)
        {
           
        }

        public Task UpdateUserAsync(InsertUserDTO userDto)
        {

        }
    }
}
