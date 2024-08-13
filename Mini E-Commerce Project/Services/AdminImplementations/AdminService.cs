using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Repositories.Implementations;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using Mini_E_Commerce_Project.Services.AdminInterfaces;
using UnauthorizedAccessException = System.UnauthorizedAccessException;


namespace Mini_E_Commerce_Project.Services.AdminImplementations
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IPaymentRepository _paymentRepository;


        public AdminService()
        {
            _userRepository = new UserRepository();
            _productRepository = new ProductRepository();
            _orderRepository = new OrderRepository();
            _orderDetailRepository = new OrderDetailRepository();
            _paymentRepository = new PaymentRepository();
        }
        public async Task DeleteUserAsync(int id, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can delete users");
            }

            var user = await _userRepository.GetSingleAsync(u => u.Id == id);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();

        }

        public async Task<List<GetUserDTO>> GetAllUsersAsync(User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can delete users");
            }

            var users = await _userRepository.GetAllAsync();
            var userDTOs = new List<GetUserDTO>();

            foreach (var user in users)
            {
                GetUserDTO userDTO = new GetUserDTO()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Address = user.Address,
                    isAdmin = user.isAdmin
                };
            }
            return userDTOs;
        }

        public async Task<GetUserDTO> GetUserByIdAsync(int id, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can delete users");
            }

            var user = await _userRepository.GetSingleAsync(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            return new GetUserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address,

            };

        }

        public async Task UpdateUserAsync(InsertUserDTO updateUser, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can delete users");
            }
            var user = await _userRepository.GetSingleAsync(u => u.Id == updateUser.Id);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<Product> CreateProductAsync(CreateProductDTO createProductDTO, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var doesExist = await _productRepository.ExistsAsync(p => p.Name == createProductDTO.Name);
            if (doesExist)
            {
                throw new InvalidProductException("This product already exists.");
            }

            Product product = new()
            {
                Name = createProductDTO.Name,
                Price = createProductDTO.Price,
                Stock = createProductDTO.Stock,
                Description = createProductDTO.Description,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            await _productRepository.CreateAsync(product);
            await _productRepository.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProductAsync(InsertProductDTO updateProductDTO, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var product = await _productRepository.GetSingleAsync(p => p.Id == updateProductDTO.Id);

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            if (string.IsNullOrWhiteSpace(updateProductDTO.Name) || updateProductDTO.Price < 0)
            {
                throw new InvalidProductException("Invalid product data.");
            }

            product.Name = updateProductDTO.Name;
            product.Price = updateProductDTO.Price;
            product.Stock = updateProductDTO.Stock;
            product.Description = updateProductDTO.Description;
            product.UpdatedDate = DateTime.UtcNow;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            return product;
        }

        public async Task DeleteProductAsync(int id, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var product = await _productRepository.GetSingleAsync(p => p.Id == id);

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<GetProductDTO> GetProductByIdAsync(int id, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var product = await _productRepository.GetSingleAsync(p => p.Id == id);

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            return new GetProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
            };
        }

        public async Task<List<GetProductDTO>> GetAllProductsAsync(User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var products = await _productRepository.GetAllAsync();
            var productDTOs = new List<GetProductDTO>();

            foreach (var product in products)
            {
                productDTOs.Add(new GetProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    Description = product.Description,
                });
            }

            return productDTOs;
        }

        // ORDER MANAGEMENT
        public async Task<List<GetOrderDTO>> GetAllOrdersAsync(User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var orders = await _orderRepository.GetAllAsync();
            var orderDTOs = new List<GetOrderDTO>();

            foreach (var order in orders)
            {
                orderDTOs.Add(new GetOrderDTO
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status,
                });
            }

            return orderDTOs;
        }

        public async Task<GetOrderDTO> GetOrderByIdAsync(int id, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var order = await _orderRepository.GetSingleAsync(o => o.Id == id);

            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            return new GetOrderDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
            };
        }

        public async Task<InsertOrderDTO> UpdateOrderStatusAsync(int orderId, StatusEnum orderStatus, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            order.Status = orderStatus;
            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();

            return new InsertOrderDTO
            {
                Id = order.Id,
                Status = order.Status
            };
        }

        public async Task<Order> DeleteOrderAsync(int orderId, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            _orderRepository.Delete(order);
            await _orderRepository.SaveChangesAsync();

            return order;
        }

        // ORDER DETAIL MANAGEMENT
        public async Task<GetOrderDetailDTO> GetOrderDetailByIdAsync(int id, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var orderDetail = await _orderDetailRepository.GetSingleAsync(od => od.Id == id);

            if (orderDetail == null)
            {
                throw new NotFoundException("Order detail not found.");
            }

            return new GetOrderDetailDTO
            {
                Id = orderDetail.Id,
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                PricePerItem = orderDetail.PricePerItem,
            };
        }

        public async Task<InsertOrderDetailDTO> UpdateOrderDetailByIdAsync(InsertOrderDetailDTO updateOrderDetailDTO, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var orderDetail = await _orderDetailRepository.GetSingleAsync(od => od.Id == updateOrderDetailDTO.Id);

            if (orderDetail == null)
            {
                throw new NotFoundException("Order detail not found.");
            }

            orderDetail.Quantity = updateOrderDetailDTO.Quantity;
            orderDetail.PricePerItem = updateOrderDetailDTO.PricePerItem;

            _orderDetailRepository.Update(orderDetail);
            await _orderDetailRepository.SaveChangesAsync();

            return new InsertOrderDetailDTO
            {
                Id = orderDetail.Id,
                Quantity = orderDetail.Quantity,
                PricePerItem = orderDetail.PricePerItem
            };
        }

        public async Task<OrderDetail> DeleteOrderDetailAsync(int id, User currentUser)
        {
            if (!currentUser.isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins can create products.");
            }

            var orderDetail = await _orderDetailRepository.GetSingleAsync(od => od.Id == id);

            if (orderDetail == null)
            {
                throw new NotFoundException("Order detail not found.");
            }

            _orderDetailRepository.Delete(orderDetail);
            await _orderDetailRepository.SaveChangesAsync();

            return orderDetail;
        }
    }

}
}
