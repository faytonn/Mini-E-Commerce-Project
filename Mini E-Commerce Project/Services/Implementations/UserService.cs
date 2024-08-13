using Mini_E_Commerce_Project.DTO.GetDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Repositories.Implementations;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using Mini_E_Commerce_Project.Services.Interfaces;

namespace Mini_E_Commerce_Project.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public async Task RegisterAsync(CreateUserDTO registerUser)
        {
            if (registerUser is null)
            {
                throw new InvalidUserInformationException("Invalid user data.");
            }

            var doesExist = await _userRepository.ExistsAsync(u => u.Email == registerUser.Email);
            if (doesExist)
            {
                throw new InvalidUserInformationException("This user already exists.");
            }

            User user = new()
            {
                FullName = registerUser.FullName,
                Email = registerUser.Email,
                Password = registerUser.Password,
                Address = registerUser.Address,
            };
            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<User> LoginAsync(User loginUser)
        {
            var user = await _userRepository.GetSingleAsync(u => u.Email.ToLower() == loginUser.Email.ToLower() && u.Password == loginUser.Password);

            if (user is null)
            {
                throw new InvalidUserInformationException("User is not true");
            }
            //if(user.Email != loginUser.Email || user.Password == loginUser.Password) 
            //{
            //    throw new InvalidLoginException("Email or password is invalid.");
            //}

            //|
            // -> One of the options to validate user.
            return user;
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

        

        public async Task UpdateUserAsync(InsertUserDTO updateUser)
        {
            var user = await _userRepository.GetSingleAsync(u => u.Id == updateUser.Id);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            User updatedUser = new()
            {
                FullName = updateUser.FullName,
                Email = updateUser.Email,
                Address = updateUser.Address

            };

             _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

        }
    }
}
