using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Services.AdminInterfaces
{
    public interface IOrderDetailService
    {
        Task CreateOrderDetailAsync(CreateOrderDetailDTO CreateOrderDetailDTO);
        Task UpdateOrderDetailAsync(InsertOrderDetailDTO UpdateOrderDetailDTO);
        Task DeletePaymentAsync(int id);
        Task<List<OrderDetail>> GetAllOrderDetailsAsync();
        Task<GetOrderDetailDTO> GetOrderDetailByIdAsync(int id);

    }
}
