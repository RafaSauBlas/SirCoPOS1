using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Messages
{
    class LoginResponse
    {
        public bool Success { get; set; }
        public Common.Entities.Empleado Empleado { get; set; }
    }
}
