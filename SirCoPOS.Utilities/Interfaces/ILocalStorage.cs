using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Interfaces
{
    public interface ILocalStorage
    {
        void SetGID(Guid gid);
        DataAccess.DataObjects.Venta LoadVenta(int id);
        void Clear();
        void AddCupon(string cupon);
        void RemoveCupon(string cupon);
        void ClearCliente();
        void RemoveArticulo(string ser);
        void AddArticulo(string serie);
        void AddCliente(int? id);
        void AddCliente(Entities.NuevoCliente nuevoCliente);
        void UpdateVendedor(int id);
    }
}
