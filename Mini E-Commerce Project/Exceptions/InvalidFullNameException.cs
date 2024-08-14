using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_E_Commerce_Project.Exceptions
{
    public class InvalidFullNameException :Exception
    {
        public InvalidFullNameException(string message) : base(message)
        {
            
        }
    }
}
