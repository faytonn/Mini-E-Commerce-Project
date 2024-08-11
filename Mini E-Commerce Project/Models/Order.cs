using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Models.Common;

namespace Mini_E_Commerce_Project.Models
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public User? Users { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public StatusEnum Status { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
        

    }
}
