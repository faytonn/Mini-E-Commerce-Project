﻿using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Repositories.Implementations;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using Mini_E_Commerce_Project.Services.AdminInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_E_Commerce_Project.Services.AdminImplementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
            _orderDetailRepository = new OrderDetailRepository();
            _productRepository = new ProductRepository();
            _userRepository = new UserRepository();
        }


        public async Task<Order> CreateOrderAsync(CreateOrderDTO createOrderDTO, User currentUser)
        {

            var user = await _userRepository.GetSingleAsync(u => u.Id == currentUser.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }


            if (createOrderDTO.TotalAmount <= 0)
            {
                throw new InvalidOrderException("Order amount must be greater than zero.");
            }

            var order = new Order
            {
                UserId = currentUser.Id,
                OrderDate = DateTime.UtcNow,
                TotalAmount = createOrderDTO.TotalAmount,
                Status = StatusEnum.Pending
            };

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task CancelOrderAsync(int orderId, User currentUser)
        {
            var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId);
            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            if (order.Status == StatusEnum.Canceled)
            {
                throw new OrderAlreadyCanceledException("Order has already been cancelled.");
            }

            order.Status = StatusEnum.Canceled;
            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task<List<GetOrderDTO>> GetUserOrdersAsync(int userId)
        {
            var orders = await _orderRepository.GetAllAsync();

            var orderDTOs = new List<GetOrderDTO>();

            foreach (var order in orders)
            {
                if (order.UserId == userId)
                {
                    var orderDetailsDTOs = order.OrderDetails.Select(detail => new CreateOrderDetailDTO
                    {
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        PricePerItem = detail.PricePerItem,
                    }).ToList();

                    var orderDTO = new GetOrderDTO
                    {
                        Id = order.Id,
                        UserId = order.UserId,
                        OrderDetails = orderDetailsDTOs,
                        Status = order.Status,
                        TotalAmount = order.TotalAmount,
                    };

                    orderDTOs.Add(orderDTO);
                }
            }

            return orderDTOs;
        }

        public async Task<List<GetOrderDetailDTO>> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId);
            if (order == null)
            {
                throw new NotFoundException("Order is not found");
            }

            return order.OrderDetails.Select(od => new GetOrderDetailDTO
            {
                OrderId = od.OrderId,
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                PricePerItem = od.PricePerItem,
            }).ToList();
        }
    }
}

