using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.Common.Entities;

namespace SirCoPOS.Utilities.Interfaces
{
    public interface IClienteView
    {
        void OpenCliente(List<Cliente> lista);
    }
}
