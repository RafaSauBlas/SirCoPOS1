using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.Utilities.DataAccess.DataObjects;
using System.Data.Entity;

namespace SirCoPOS.Win.Helpers
{
    class LocalStorage : Utilities.Interfaces.ILocalStorage
    {
        private DataAccess.DataContext _ctx;
        private Utilities.DataAccess.DataObjects.Venta _venta;
        private Guid _gid;
        public LocalStorage()
            : this(Guid.NewGuid())
        { 
        
        }
        public LocalStorage(Guid gid)
        {
            _gid = gid;
            _ctx = new DataAccess.DataContext();
        }
        public void SetGID(Guid gid)
        {
            _gid = gid;
        }
        public void RemoveCupon(string cupon)
        {
            var item = _venta.Cupones.Where(i => i.Cupon == cupon).Single();
            _venta.Cupones.Remove(item);
            _ctx.SaveChanges();
        }

        public void AddCupon(string cupon)
        {
            _venta.Cupones.Add(new Utilities.DataAccess.DataObjects.VentaCupon { 
                Cupon = cupon
            });
            _ctx.SaveChanges();
        }

        public void UpdateVendedor(int id)
        {
            _venta.VendedorId = id;
            _ctx.SaveChanges();
        }

        public void Clear()
        {
            var ven = _ctx.Ventas.Where(i => i.Id == _gid).SingleOrDefault();
            if (ven == null)
                return;
            var items = _venta.Articulos.Select(i => i.Serie).ToList();
            _ctx.Ventas.Remove(_venta);
            _ctx.SaveChanges();
        }

        public void RemoveArticulo(string ser)
        {
            var del = _venta.Articulos.Where(i => i.Serie == ser).Single();
            _venta.Articulos.Remove(del);
            _ctx.SaveChanges();
        }

        public void AddArticulo(string serie)
        {
            _venta.Articulos.Add(new Utilities.DataAccess.DataObjects.VentaArticulo { Serie = serie });
            _ctx.SaveChanges();
        }

        internal void ClearCajero()
        {
            //TODO pendiente release
            var setting = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
            var q = _ctx.Ventas
                .Include(i => i.Articulos)
                .Include(i => i.Cupones)
                .Include(i => i.Pagos)
                .Where(i => i.CajeroId == setting.Cajero.Id);
            foreach (var item in q.ToArray())
            {                
                foreach (var i in item.Articulos.ToArray())
                {
                    _ctx.VentaArticulos.Remove(i);
                }
                foreach (var i in item.Cupones.ToArray())
                {
                    _ctx.VentaCupones.Remove(i);
                }
                foreach (var i in item.Pagos.ToArray())
                {
                    _ctx.VentaPagos.Remove(i);
                }
                _ctx.Ventas.Remove(item);
            }
            _ctx.SaveChanges();
        }

        internal void RemovePago(Guid id)
        {
            var item = _venta.Pagos.Where(i => i.Id == id).Single();
            _venta.Pagos.Remove(item);
            _ctx.VentaPagos.Remove(item);
            _ctx.SaveChanges();
        }

        public Venta LoadVenta(int id)
        {
            _venta = _ctx.Ventas
                .Include(i => i.Articulos)
                .Include(i => i.Cupones)
                .Include(i => i.Pagos)
                .Where(i => i.Id == _gid).SingleOrDefault();
            if (_venta == null)
            {
                _venta = new Utilities.DataAccess.DataObjects.Venta
                {
                    Id = _gid,
                    CajeroId = id,
                    Fecha = DateTime.Now,
                    Articulos = new HashSet<VentaArticulo>(),
                    Cupones = new HashSet<VentaCupon>(),
                    Pagos = new HashSet<VentaPago>()
                };
                _ctx.Ventas.Add(_venta);
                _ctx.SaveChanges();
            }
            return _venta;
        }

        public void AddCliente(int? id)
        {
            _venta.ClienteId = id;
            _venta.NuevoCliente = null;
            _ctx.SaveChanges();
        }

        public void AddCliente(Utilities.Entities.NuevoCliente nuevoCliente)
        {
            _venta.ClienteId = null;
            _venta.NuevoCliente = Utilities.Helpers.Serializer.Serialize(nuevoCliente);
            _ctx.SaveChanges();
        }

        internal void AddPago(Utilities.Messages.Pago p, Guid id)
        {
            var data = Utilities.Helpers.Serializer.Serialize(p);
            _venta.Pagos.Add(new VentaPago { 
                Id = id,
                FormaPago = p.FormaPago, 
                Importe = p.Importe, 
                Data = data 
            });
            _ctx.SaveChanges();
        }

        public void ClearCliente()
        {
            _venta.ClienteId = null;
            _venta.NuevoCliente = null;
            _ctx.SaveChanges();
        }

        internal void ClearAll()
        {
            //TODO pendiente release
            var setting = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
            var q = _ctx.Ventas
                .Include(i => i.Articulos)
                .Include(i => i.Cupones)
                .Include(i => i.Pagos);
            foreach (var item in q.ToArray())
            {
                foreach (var i in item.Articulos.ToArray())
                {
                    _ctx.VentaArticulos.Remove(i);
                }
                foreach (var i in item.Cupones.ToArray())
                {
                    _ctx.VentaCupones.Remove(i);
                }
                foreach (var i in item.Pagos.ToArray())
                {
                    _ctx.VentaPagos.Remove(i);
                }
                _ctx.Ventas.Remove(item);
            }
            _ctx.SaveChanges();            
        }
    }
}
