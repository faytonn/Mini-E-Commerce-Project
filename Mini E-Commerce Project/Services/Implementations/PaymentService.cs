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
        public async Task CreatePaymentAsync(CreatePaymentDTO paymentDTO, int userId)
        {

            var order = await _orderRepository.GetSingleAsync(x => x.UserId == userId && x.Status == Enums.StatusEnum.Pending, "OrderDetails.Product", "User");


            if (order is null)
                throw new NotFoundException("Order is not found");

            if (order.OrderDetails.Count == 0)
                throw new NotFoundException("Order is empty");

            //if ( <= 0)
            //{
            //    throw new InvalidPaymentException("Amount should be greater than zero.");
            //}
            if (paymentDTO.Amount != order.TotalAmount)
            {
                throw new InvalidPaymentException("Payment amount does not match order total.");
            }

            decimal totalCount = 0;


            order.OrderDetails.ForEach(x => totalCount +=x.Quantity* x.Product.Price);


            Payment payment = new()
            {
                Amount = totalCount,
                OrderId = order.Id,
                PaymentDate = DateTime.Now,

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
