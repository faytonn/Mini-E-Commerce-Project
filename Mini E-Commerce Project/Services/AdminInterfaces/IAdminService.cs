using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Services.AdminInterfaces
{
    public interface IAdminService
    {
        // USER MANAGEMENT
        Task<List<GetUserDTO>> GetAllUsersAsync(User currentUser);
        Task<GetUserDTO> GetUserByIdAsync(int id, User currentUser);
        Task DeleteUserAsync(int id, User currentUser);
        Task UpdateUserAsync(InsertUserDTO userDto, User currentUser);

        // PRODUCT MANAGEMENT
        Task<Product> CreateProductAsync(CreateProductDTO createProductDTO, User currentUser);
        Task<Product> UpdateProductAsync(InsertProductDTO updateProductDTO, User currentUser);
        Task DeleteProductAsync(int id, User currentUser);
        Task<GetProductDTO> GetProductByIdAsync(int id, User currentUser);
        Task<List<GetProductDTO>> GetAllProductsAsync(User currentUser);

        // ORDER MANAGEMENT
        Task<List<GetOrderDTO>> GetAllOrdersAsync(User currentUser);
        Task<GetOrderDTO> GetOrderByIdAsync(int id, User currentUser);
        Task<InsertOrderDTO> UpdateOrderStatusAsync(int orderId, StatusEnum orderStatus, User currentUser);
        Task<Order> DeleteOrderAsync(int orderId, User currentUser);

        // ORDER DETAIL MANAGAMENT
        Task<GetOrderDetailDTO> GetOrderDetailByIdAsync(int id, User currentUser);
        Task<InsertOrderDetailDTO> UpdateOrderDetailByIdAsync(InsertOrderDetailDTO updateOrderDetailDTO, User currentUser);
        Task<OrderDetail> DeleteOrderDetailAsync(int id, User currentUser);

        // PAYMENT MANAGEMENT



    }
}
