using Mini_E_Commerce_Project.DTO.GetDTO.AdminAccessedDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;

namespace Mini_E_Commerce_Project.Services.AdminInterfaces
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(CreatePaymentDTO paymentDTO, int userId);
        Task<GetPaymentDTOAdmin> GetPaymentByIdAsync(int id);


    }
}
