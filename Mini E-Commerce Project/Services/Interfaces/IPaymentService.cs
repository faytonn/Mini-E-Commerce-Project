using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;

namespace Mini_E_Commerce_Project.Services.AdminInterfaces
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(CreatePaymentDTO paymentDTO);
        Task<GetPaymentDTO> GetPaymentByIdAsync(int id);

    }
}
