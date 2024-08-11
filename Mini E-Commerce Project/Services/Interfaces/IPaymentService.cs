using Mini_E_Commerce_Project.DTO;

namespace Mini_E_Commerce_Project.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<List<PaymentDTO>> GetAllPaymentsAsync();
        Task<PaymentDTO> GetPaymentByIdAsync(int id);
        Task CreatePaymentAsync(PaymentDTO paymentDTO);
        Task UpdatePaymentAsync(PaymentDTO paymentDTO);
        Task DeletePaymentAsync(int id);
    }
}
