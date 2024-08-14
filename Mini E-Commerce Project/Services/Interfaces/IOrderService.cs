using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_E_Commerce_Project.Services.AdminInterfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderDTO orderDTO, User currentUser);
        Task<List<Order>> GetUserOrdersAsync();
        Task<GetOrderDTO> GetOrderByIdAsync(int id);
        Task CancelOrderAsync(int orderId, User currentUser);
    }
}
