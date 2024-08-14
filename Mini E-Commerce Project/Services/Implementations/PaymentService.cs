using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.InsertDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
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
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public PaymentService()
        {
            _paymentRepository = new PaymentRepository();
            _orderRepository = new OrderRepository();
            _userRepository = new UserRepository();
        }
        public async Task CreatePaymentAsync(CreatePaymentDTO paymentDTO)
        {
            if (paymentDTO.Amount <= 0)
            {
                throw new InvalidPaymentException("Amount should be greater than zero.");
            }

            var order = await _orderRepository.GetSingleAsync(o => o.Id == paymentDTO.OrderId && o.UserId == currentUser.Id);
            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            if (paymentDTO.Amount != order.TotalAmount)
            {
                throw new InvalidPaymentException("Payment amount does not match order total.");
            }

            var payment = new Payment
            {
                OrderId = paymentDTO.OrderId,
                Amount = paymentDTO.Amount,
                PaymentDate = DateTime.UtcNow
            };

            await _paymentRepository.CreateAsync(payment);
            await _paymentRepository.SaveChangesAsync();
        }

        public Task<GetPaymentDTO> GetPaymentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
