using Mini_E_Commerce_Project.DTO.GetDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_E_Commerce_Project.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDTO CreateOrderDTO);
        Task UpdateOrderAsync(InsertOrderDTO UpdateOrderDTO);
        Task DeletePaymentAsync(int id);
        Task<List<Order>> GetAllOrdersAsync();
        Task<GetOrderDTO> GetOrderByIdAsync(int id);
    }
}
