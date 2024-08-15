using Mini_E_Commerce_Project.DTO.GetDTO.AdminAccessedDTO;
using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Repositories.Implementations;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using Mini_E_Commerce_Project.Services.ADMIN.AdminInterfaces;
using DocumentFormat.OpenXml.Drawing.Charts;



namespace Mini_E_Commerce_Project.Services.ADMIN.AdminImplementations;

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

    //USER MANAGEMENT
    public async Task DeleteUserAsync(int id, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can delete users");
        }

        var user = await _userRepository.GetSingleAsync(u => u.Id == id);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();

    }

    public async Task<List<GetUserDTOAdmin>> GetAllUsersAsync(User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can delete users");
        }

        var users = await _userRepository.GetAllAsync();
        var userDTOs = new List<GetUserDTOAdmin>();

        foreach (var user in users)
        {
            GetUserDTOAdmin userDTO = new GetUserDTOAdmin()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address,
                isAdmin = user.isAdmin
            };
            userDTOs.Add(userDTO);
        }
        return userDTOs;
    }

    public async Task<GetUserDTOAdmin> GetUserByIdAsync(int id, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can delete users");
        }

        var user = await _userRepository.GetSingleAsync(u => u.Id == id);

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        return new GetUserDTOAdmin
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Address = user.Address,
            isAdmin = user.isAdmin
        };

    }


    // PRODUCT MANAGEMENT
    public async Task<Product> CreateProductAsync(CreateProductDTO createProductDTO, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can create products.");
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
            Description = createProductDTO.Description,
            Stock = createProductDTO.Stock,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow

        };

        await _productRepository.CreateAsync(product);
        await _productRepository.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateProductAsync(InsertProductDTO updateProductDTO, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can create products.");
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
            throw new UnAuthorizedAccessException("Only admins can create products.");
        }

        var product = await _productRepository.GetSingleAsync(p => p.Id == id);

        if (product == null)
        {
            throw new NotFoundException("Product not found.");
        }

        _productRepository.Delete(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task<GetProductDTOAdmin> GetProductByIdAsync(int id, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can create products.");
        }

        var product = await _productRepository.GetSingleAsync(p => p.Id == id);

        if (product == null)
        {
            throw new NotFoundException("Product not found.");
        }

        return new GetProductDTOAdmin
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Description = product.Description,
            CreatedDate = product.CreatedDate,
            UpdatedDate = product.UpdatedDate,
        };
    }

    public async Task<List<GetProductDTOAdmin>> GetAllProductsAsync(User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can create products.");
        }

        var products = await _productRepository.GetAllAsync();
        var productDTOs = new List<GetProductDTOAdmin>();

        foreach (var product in products)
        {
            productDTOs.Add(new GetProductDTOAdmin
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate,
            });
        }

        return productDTOs;
    }

    // ORDER MANAGEMENT
    public async Task<List<GetOrderDTOAdmin>> GetAllOrdersAsync(User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can create products.");
        }

        var orders = await _orderRepository.GetAllAsync();
        var orderDTOs = new List<GetOrderDTOAdmin>();

        foreach (var order in orders)
        {
            orderDTOs.Add(new GetOrderDTOAdmin
            {
                Id = order.Id,
                UserId = order.UserId,
                UsersName = order.Users.FullName,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderDetails = order.OrderDetails,
            }); ;
        }

        return orderDTOs;
    }

    public async Task<GetOrderDTOAdmin> GetOrderByIdAsync(int id, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can create products.");
        }

        var order = await _orderRepository.GetSingleAsync(o => o.Id == id);

        if (order == null)
        {
            throw new NotFoundException("Order not found.");
        }

        return new GetOrderDTOAdmin
        {
            Id = order.Id,
            UserId = order.UserId,
            UsersName = order.Users.FullName,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            OrderDetails = order.OrderDetails
        };
    }

    public async Task<InsertOrderDTO> UpdateOrderStatusAsync(int orderId, StatusEnum orderStatus, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins have the permission for this.");
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

    public async Task DeleteOrderAsync(int orderId, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can create products.");
        }

        var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId);

        if (order == null)
        {
            throw new NotFoundException("Order not found.");
        }

        _orderRepository.Delete(order);
        await _orderRepository.SaveChangesAsync();
    }
    public async Task CompleteOrderAsync(int orderId, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can complete orders.");
        }

        var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId);

        if (order == null)
        {
            throw new NotFoundException("Order not found.");
        }



        order.Status = StatusEnum.Completed;
        _orderRepository.Update(order);
        await _orderRepository.SaveChangesAsync();
    }
    public async Task<List<GetOrderDetailDTOAdmin>> GetOrderDetailsByOrderIdAsync(int orderId, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can view order details.");
        }

        var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId);
        if (order == null)
        {
            throw new NotFoundException("Order not found.");
        }

        var allOrderDetails = await _orderDetailRepository.GetAllAsync();

        var orderDetails = new List<OrderDetail>();
        foreach (var detail in allOrderDetails)
        {
            if (detail.OrderId == orderId)
            {
                orderDetails.Add(detail);
            }
        }

        var orderDetailDTOs = new List<GetOrderDetailDTOAdmin>();
        foreach (var detail in orderDetails)
        {
            var dto = new GetOrderDetailDTOAdmin
            {
                OrderId = detail.OrderId,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                PricePerItem = detail.PricePerItem,

            };
            orderDetailDTOs.Add(dto);
        }

        return orderDetailDTOs;
    }

    // ORDER DETAIL MANAGEMENT
    public async Task<GetOrderDetailDTO> GetOrderDetailByIdAsync(int id, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can create products.");
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
            throw new UnAuthorizedAccessException("Only admins can access this place.");
        }

        var orderDetail = await _orderDetailRepository.GetSingleAsync(od => od.Id == updateOrderDetailDTO.Id);

        if (orderDetail == null)
        {
            throw new NotFoundException("Order detail not found.");
        }

        
        orderDetail.ProductId = updateOrderDetailDTO.ProductId;
        orderDetail.Quantity = updateOrderDetailDTO.Quantity;
        orderDetail.PricePerItem = updateOrderDetailDTO.PricePerItem;

        _orderDetailRepository.Update(orderDetail);
        await _orderDetailRepository.SaveChangesAsync();

        return new InsertOrderDetailDTO
        {
            ProductId = orderDetail.ProductId,
            Quantity = orderDetail.Quantity,
            PricePerItem = orderDetail.PricePerItem
        };
    }

    public async Task<OrderDetail> DeleteOrderDetailAsync(int id, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can create products.");
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



    //PAYMENT MANAGEMENT
    public async Task<List<GetPaymentDTOAdmin>> GetAllPayments()
    {
        var payments = await _paymentRepository.GetAllAsync();
        var paymentDTOs = new List<GetPaymentDTOAdmin>();


        foreach (var payment in payments)
        {

            GetPaymentDTOAdmin dto = new GetPaymentDTOAdmin
            {
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate
            };

            paymentDTOs.Add(dto);
        }
        return paymentDTOs;
    }


    public async Task RefundPaymentAsync(int paymentId, decimal refundAmount, User currentUser)
    {
        if (!currentUser.isAdmin)
        {
            throw new UnAuthorizedAccessException("Only admins can process refunds.");
        }

        var payment = await _paymentRepository.GetSingleAsync(p => p.Id == paymentId);
        if (payment == null)
        {
            throw new NotFoundException("Payment not found.");
        }

        if (refundAmount <= 0 || refundAmount > payment.Amount)
        {
            throw new InvalidPaymentException("Invalid refund amount.");
        }


        payment.Amount -= refundAmount;

        _paymentRepository.Update(payment);
        await _paymentRepository.SaveChangesAsync();
    }
}



