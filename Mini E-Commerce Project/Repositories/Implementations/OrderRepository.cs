using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Repositories.Interfaces;

namespace Mini_E_Commerce_Project.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderdetailRepository
    {
    }
}
