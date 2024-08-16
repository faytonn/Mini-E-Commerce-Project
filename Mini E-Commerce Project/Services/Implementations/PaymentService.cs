using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Mini_E_Commerce_Project.DTO.GetDTO.AdminAccessedDTO;
using Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO;
using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Exceptions;
using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Repositories.Implementations;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using Mini_E_Commerce_Project.Services.AdminInterfaces;
using Mini_E_Commerce_Project.Utilities;
using Colored = Mini_E_Commerce_Project.Utilities.Colored;

namespace Mini_E_Commerce_Project.Services.AdminImplementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public PaymentService()
        {
            _paymentRepository = new PaymentRepository();
            _orderRepository = new OrderRepository();
            _userRepository = new UserRepository();
            _productRepository = new ProductRepository();
        }
        public async Task CreatePaymentAsync(CreatePaymentDTO paymentDTO, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User not found.");

            var order = await _orderRepository.GetSingleAsync(x => x.UserId == userId && x.Status == Enums.StatusEnum.Pending, "OrderDetails.Product", "User");

            if (order is null)
                throw new NotFoundException("Order is not found");

            if (order.OrderDetails.Count == 0)
                throw new NotFoundException("Order is empty");

            if (paymentDTO.Amount <= 0)
                throw new InvalidPaymentException("Amount should be greater than zero.");

            if (paymentDTO.Amount != order.TotalAmount)
                throw new InvalidPaymentException("Payment amount does not match order total.");

            if (user.Balance < paymentDTO.Amount)
                throw new InvalidPaymentException("Insufficient balance to complete the order.");


            user.Balance -= paymentDTO.Amount;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();


            foreach (var detail in order.OrderDetails)
            {
                int newStock = detail.Product.Stock - detail.Quantity;

                if (newStock >= 0)
                {
                    int leftStock = detail.Product.Stock = detail.Product.Stock - detail.Quantity;
                }
                else
                {
                    Colored.WriteLine($"Not enough products available in stock, hence only {detail.Product.Stock} of {detail.Product} was added to your order.", ConsoleColor.DarkYellow);
                    detail.Quantity = detail.Product.Stock;
                    detail.Product.Stock = 0;
                }

                _productRepository.Update(detail.Product);
                await _productRepository.SaveChangesAsync();
            }


            decimal totalCount = 0;

            order.OrderDetails.ForEach(x => totalCount += x.Quantity * x.Product.Price);


            Payment payment = new()
            {
                Amount = totalCount,
                OrderId = order.Id,
                PaymentDate = DateTime.Now,

            };

            _orderRepository.Update(order);

            await _paymentRepository.CreateAsync(payment);
            await _paymentRepository.SaveChangesAsync();
        }

        public async Task<GetPaymentDTOAdmin> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetSingleAsync(x => x.Id == id);

            if (payment == null)
                throw new NotFoundException("Payment not found.");

            GetPaymentDTOAdmin paymentDTO = new GetPaymentDTOAdmin()
            {
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
            };

            return paymentDTO;
        }
    }
}
