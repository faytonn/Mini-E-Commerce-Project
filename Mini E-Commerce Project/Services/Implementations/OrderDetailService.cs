using DocumentFormat.OpenXml.Spreadsheet;
using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Repositories.Implementations;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using Mini_E_Commerce_Project.Services.AdminInterfaces;

namespace Mini_E_Commerce_Project.Services.AdminImplementations
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderDetailService()
        {
            _orderDetailRepository = new OrderDetailRepository();
            _userRepository = new UserRepository();
            _orderRepository = new OrderRepository();
        }

        public async Task<List<GetOrderDetailDTO>> GetAllOrderDetailsAsync(int userId)
        {
            var orders = await _orderRepository.GetAllAsync("OrderDetails");

            if (orders == null || orders.Count == 0)
                throw new NotFoundException("No orders found for this user.");

            var orderDetailsDTOs = new List<GetOrderDetailDTO>();
            foreach(var order in orders)
            {
                if(order.UserId == userId)
                {
                    foreach(var detail in order.OrderDetails)
                    {
                        GetOrderDetailDTO odDTO = new GetOrderDetailDTO()
                        {
                            OrderId = detail.OrderId,
                            ProductId = detail.ProductId,
                            Quantity = detail.Quantity,
                            PricePerItem = detail.PricePerItem,
                        };
                        orderDetailsDTOs.Add(odDTO);
                    }
                }
            }
            return orderDetailsDTOs;
        }

        public async Task<GetOrderDetailDTO> GetOrderDetailByIdAsync(int id, int userId)
        {
            var orderDetail = await _orderDetailRepository.GetSingleAsync(od => od.Id == id, "Order");
            if (orderDetail == null)
            {
                throw new NotFoundException("Order detail not found.");
            }

            var order = await _orderRepository.GetByIdAsync(orderDetail.OrderId);
            if (order == null || order.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to view this order detail.");
            }

            return new GetOrderDetailDTO
            {
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                PricePerItem = orderDetail.PricePerItem,
            };
        }
    }
}
