using Mini_E_Commerce_Project.DTO.ServiceDTO;
using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_E_Commerce_Project.DTO.GetDTO
{
    public record GetOrderDTO
    {
        public int UserId { get; set; }
        public List<CreateOrderDetailDTO>? OrderDetails { get; set; }
        public decimal TotalAmount { get; set; }
        public StatusEnum Status { get; set; }

    }
}
