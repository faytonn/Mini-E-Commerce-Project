using Mini_E_Commerce_Project.Models;
using Mini_E_Commerce_Project.Repositories.Implementations.Generic;
using Mini_E_Commerce_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_E_Commerce_Project.Repositories.Implementations
{
    public class AdminRepository : Repository<User>, IAdminRepository
    {
    }
}
