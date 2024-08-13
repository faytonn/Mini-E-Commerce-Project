using Mini_E_Commerce_Project.DTO.GetDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Services.Interfaces
{
    public interface IAdminService 
    {
        // USER MANAGEMENT
        Task<List<GetUserDTO>> GetAllUsersAsync();
        Task<GetUserDTO> GetUserByIdAsync(int id);
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(InsertUserDTO userDto);

        // PRODUCT MANAGEMENT
        Task<Product> CreateProductAsync(CreateProductDTO createProductDTO);
        Task<Product> UpdateProductAsync(InsertProductDTO updateProductDTO);
        Task DeleteProductAsync(int id);
        Task<GetProductDTO> GetProductByIdAsync(int id);
        Task<List<GetProductDTO>> GetAllProductsAsync();

        // ORDER MANAGEMENT
        Task<List<GetOrderDTO>> GetAllOrdersAsync();
        Task<GetOrderDTO> GetOrderByIdAsync(int id);
        Task<Product> UpdateOrderStatusAsync(int orderId, StatusEnum orderStatus);
        Task<Product> DeleteOrderAsync(int orderId);

        // ORDER DETAIL MANAGAMENT
        Task<GetOrderDetailDTO> GetOrderDetailByIdAsync(int id);
        Task<InsertOrderDetailDTO> UpdateOrderDetailAsync(InsertOrderDetailDTO updateOrderDetailDTO);
        Task<OrderDetail> DeleteOrderDetailAsync(int id);

        // PAYMENT MANAGEMENT


    }
}
