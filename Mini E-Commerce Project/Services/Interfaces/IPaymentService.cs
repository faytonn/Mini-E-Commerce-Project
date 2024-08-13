using Mini_E_Commerce_Project.DTO.GetDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;

namespace Mini_E_Commerce_Project.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<List<GetPaymentDTO>> GetAllPaymentsAsync();
        Task<GetPaymentDTO> GetPaymentByIdAsync(int id);
        Task CreatePaymentAsync(CreatePaymentDTO paymentDTO);
        Task UpdatePaymentAsync(InsertPaymentDTO paymentDTO);
        Task DeletePaymentAsync(int id);
    }
}
