﻿using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.DTO.GetDTO.UserAccessedDTO
{
    public record GetOrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public List<Order>? Orders { get; set; }
        public int ProductId { get; set; }
        public List<Product>? Products { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public StatusEnum Status { get; set; }
    }
}
