using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Services.AdminInterfaces;

namespace Mini_E_Commerce_Project.Services.AdminImplementations
{
    internal class OrderDetailService : IOrderDetailService
    {
        public Task CreateOrderDetailAsync(CreateOrderDetailDTO CreateOrderDetailDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeletePaymentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDetail>> GetAllOrderDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetOrderDetailDTO> GetOrderDetailByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderDetailAsync(InsertOrderDetailDTO UpdateOrderDetailDTO)
        {
            throw new NotImplementedException();
        }
    }
}
