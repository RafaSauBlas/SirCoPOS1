using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Interfaces
{
    public interface ITabViewModel
    {
        Constants.TabType TabType { get; set; }
        Sucursal Sucursal { get; }
        Empleado Cajero { get; }
        Guid GID { get; }
        void Init(Guid gid);
        void Close();
    }
}
