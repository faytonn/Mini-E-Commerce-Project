using Mini_E_Commerce_Project.DTO.GetDTO.AdminAccessedDTO;
using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Services.ADMIN.AdminInterfaces
{
    public interface IAdminService
    {
        // USER MANAGEMENT
        Task<List<GetUserDTOAdmin>> GetAllUsersAsync(User currentUser);
        Task<GetUserDTOAdmin> GetUserByIdAsync(int id, User currentUser);
        Task DeleteUserAsync(int id, User currentUser);

        // PRODUCT MANAGEMENT
        Task<Product> CreateProductAsync(CreateProductDTO createProductDTO, User currentUser);
        Task<Product> UpdateProductAsync(InsertProductDTO updateProductDTO, User currentUser);
        Task DeleteProductAsync(int id, User currentUser);
        Task<GetProductDTOAdmin> GetProductByIdAsync(int id, User currentUser);
        Task<List<GetProductDTOAdmin>> GetAllProductsAsync(User currentUser);

        // ORDER MANAGEMENT
        Task CompleteOrderAsync(int orderId, User currentUser);
        Task<List<GetOrderDTOAdmin>> GetAllOrdersAsync();
        Task<GetOrderDTOAdmin> GetOrderByIdAsync(int id, User currentUser);
        Task<InsertOrderDTO> UpdateOrderStatusAsync(int orderId, StatusEnum orderStatus, User currentUser);
        Task DeleteOrderAsync(int orderId, User currentUser);
        Task<List<GetOrderDetailDTOAdmin>> GetOrderDetailsByOrderIdAsync(int orderId, User currentUser);

        // ORDER DETAIL MANAGAMENT
        Task<GetOrderDetailDTO> GetOrderDetailByIdAsync(int id, User currentUser);
        Task<InsertOrderDetailDTO> UpdateOrderDetailByIdAsync(InsertOrderDetailDTO updateOrderDetailDTO, User currentUser);
        Task<OrderDetail> DeleteOrderDetailAsync(int id, User currentUser);

        // PAYMENT MANAGEMENT
        Task<List<GetPaymentDTOAdmin>> GetAllPayments(User currentUser);
        Task RefundPaymentAsync(int paymentId, User currentUser);


    }
}
