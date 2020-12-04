using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Tests.ViewModels
{
    class LoginViewModel
    {
        public LoginViewModel()
        {
            this.UserName = "username";
            this.Password = "password";
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
