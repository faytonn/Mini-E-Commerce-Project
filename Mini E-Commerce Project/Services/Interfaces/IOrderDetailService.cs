using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Services.Interfaces
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> GetAllOrderDetailsAsync();
        Task CreateOrderDetail
    }
}
