using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.BusinessLogic
{
    public class Data
    {
        public T GetParametro<T>(string key, string sucursal = null)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
                throw new NotSupportedException();
            
            var ctx = new DataAccess.SirCoControlDataContext();
            var q = ctx.Parametros.Where(i => i.clave == key);
            var qs = q.Where(i => i.sucursal == sucursal);
            if (qs.Any())
            {
                var item = qs.Single();
                return (T)converter.ConvertFrom(item.valor);                

            }
            else
            {
                var item = q.Where(i => i.sucursal == "99").SingleOrDefault();
                if (item != null)
                    return (T)converter.ConvertFrom(item.valor);
                return default(T);
            }
        }
        public Sucursal FindSucursal(string sucursal)
        {
            var ctx = new DataAccess.SirCoControlDataContext();
            var item = ctx.Sucursales.Where(i => i.sucursal == "01").Single();
            var res = new Common.Entities.Sucursal
            {
                Id = item.idsucursal,
                Clave = item.sucursal,
                Descripcion = item.descrip
            };
            return res;
        }
        public Empleado FindCajero(string user)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var item = ctx.Empleados.Where(i => i.usuariosistema == user
                && i.iddepto == (int)Common.Constants.Departamento.TDA
                && i.idpuesto == (int)Common.Constants.Puesto.CJA
                && i.estatus == "A").SingleOrDefault();
            if (item != null)
            {
                return new Empleado
                {
                    Id = item.idempleado,
                    ApellidoMaterno = item.apmaterno,
                    ApellidoPaterno = item.appaterno,
                    Nombre = item.nombre,
                    Usuario = item.usuariosistema,
                    Clave = item.clave
                };
            }
            return null;
        }
        public Empleado FindAuditorApertura(int id, int idcajero)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var ctxpos = new DataAccess.SirCoPOSDataContext();
            var cajero = ctx.Empleados.Where(i => i.idempleado == idcajero
                && i.estatus == "A").Single();

            var auditor = ctx.Empleados.Where(i => i.idempleado == id                
                && i.estatus == "A").SingleOrDefault();
            if (auditor != null)
            {                
                var asuc = auditor.clave.Substring(0, 2);
                var suc = cajero.clave.Substring(0, 2);

                if (!(cajero.idpuesto == (int)Common.Constants.Puesto.CJA 
                    || Common.Constants.Puestos.Gerentes.Contains(cajero.idpuesto)))
                {
                    return null;
                }


                decimal? disponible = null;
                if (auditor.idpuesto == (int)Common.Constants.Puesto.CJA
                    || Common.Constants.Puestos.Gerentes.Contains(auditor.idpuesto))
                {
                    var fondo = ctxpos.Fondos.Where(i => i.CajaSucursal == suc && i.ResponsableId == auditor.idempleado
                                    && !i.FechaCierre.HasValue)
                                    .SingleOrDefault();
                    if (fondo == null)
                        return null;
                    else
                        disponible = fondo.Disponible;
                }
                else if (!Common.Constants.Puestos.Admin.Contains(auditor.idpuesto))
                {
                    return null;
                }

                return new Empleado
                {
                    Id = auditor.idempleado,
                    ApellidoMaterno = auditor.apmaterno,
                    ApellidoPaterno = auditor.appaterno,
                    Nombre = auditor.nombre,
                    Usuario = auditor.usuariosistema,
                    Clave = auditor.clave, 
                    Puesto = auditor.idpuesto,
                    Disponible = disponible
                };
            }
            return null;
        }
        public Empleado FindAuditorEntrega(int id, int idcajero)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var ctxpos = new DataAccess.SirCoPOSDataContext();
            var cajero = ctx.Empleados.Where(i => i.idempleado == idcajero
                && i.estatus == "A").Single();

            var auditor = ctx.Empleados.Where(i => i.idempleado == id
                && i.estatus == "A").SingleOrDefault();
            if (auditor != null)
            {
                var asuc = auditor.clave.Substring(0, 2);
                var suc = cajero.clave.Substring(0, 2);
                if (Common.Constants.Puestos.Gerentes.Contains(cajero.idpuesto)
                    || cajero.idpuesto == (int)Common.Constants.Puesto.CJA)
                {
                    if (Common.Constants.Puestos.Gerentes.Contains(auditor.idpuesto))
                    {
                        var fondo = ctxpos.Fondos.Where(i => i.ResponsableId == auditor.idempleado && !i.FechaCierre.HasValue).SingleOrDefault();
                        if (fondo == null)
                            return null;
                    }
                    else if (auditor.idpuesto == (int)Common.Constants.Puesto.MEN)
                    {
                        var fondo = ctxpos.Fondos.Where(i => i.ResponsableId == auditor.idempleado && !i.FechaCierre.HasValue).SingleOrDefault();
                        if (fondo == null)
                            return null;
                    }
                    else if (!Common.Constants.Puestos.Admin.Contains(auditor.idpuesto))
                        return null;
                }
                //else if (cajero.idpuesto == (int)Common.Constants.Puesto.CJA)
                //{ 
                
                //}
                else
                    return null;


                return new Empleado
                {
                    Id = auditor.idempleado,
                    ApellidoMaterno = auditor.apmaterno,
                    ApellidoPaterno = auditor.appaterno,
                    Nombre = auditor.nombre,
                    Usuario = auditor.usuariosistema,
                    Clave = auditor.clave,
                    Puesto = auditor.idpuesto
                };
            }
            return null;
        }
        public Empleado FindAuditorTransferir(int id, int idcajero)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var ctxpos = new DataAccess.SirCoPOSDataContext();
            var cajero = ctx.Empleados.Where(i => i.idempleado == idcajero
                && i.estatus == "A").Single();

            var auditor = ctx.Empleados.Where(i => i.idempleado == id
                && i.estatus == "A").SingleOrDefault();
            if (auditor != null)
            {
                var asuc = auditor.clave.Substring(0, 2);
                var suc = cajero.clave.Substring(0, 2);
                if (Common.Constants.Puestos.Gerentes.Contains(cajero.idpuesto))
                {
                    if (Common.Constants.Puestos.Gerentes.Contains(auditor.idpuesto))
                    {
                        var fondo = ctxpos.Fondos.Where(i => i.ResponsableId == auditor.idempleado && !i.FechaCierre.HasValue).SingleOrDefault();
                        if (fondo != null)
                            return null;
                    }
                    else if (auditor.idpuesto == (int)Common.Constants.Puesto.MEN)
                    {
                        return null;
                    }
                    else if (Common.Constants.Puestos.Admin.Contains(auditor.idpuesto))
                        return null;
                    else 
                        return null;
                }
                else
                    return null;


                return new Empleado
                {
                    Id = auditor.idempleado,
                    ApellidoMaterno = auditor.apmaterno,
                    ApellidoPaterno = auditor.appaterno,
                    Nombre = auditor.nombre,
                    Usuario = auditor.usuariosistema,
                    Clave = auditor.clave,
                    Puesto = auditor.idpuesto
                };
            }
            return null;
        }
        public IEnumerable<Colonia> FindColonias(string cp)
        {
            var ctx = new DataAccess.SirCoControlDataContext();
            var q = ctx.Colonias.Where(i => i.codigopostal == cp);
            var res = q.Select(i => new Common.Entities.Colonia
            {
                Id = i.idcolonia,
                Nombre = i.colonia,
                CodigoPostal = i.codigopostal,
                CiudadId = i.idciudad,
                CiudadNombre = i.Ciudad.ciudad,
                EstadoId = i.idestado,
                EstadoNombre = i.Ciudad.Estado.estado
            }).ToArray();
            return res;
        }
        public IDictionary<int, ICollection<byte[]>> Approve(string sucursal)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var q = ctx.Empleados.Where(i => 
                i.iddepto == (int)Common.Constants.Departamento.TDA
                && (i.idpuesto == (int)Common.Constants.Puesto.ENC || i.idpuesto == (int)Common.Constants.Puesto.SUP)
                && i.estatus == "A"
                && i.clave.Substring(0, 2) == sucursal);
            var res = new Dictionary<int, ICollection<byte[]>>();
            foreach (var item in q)
            {
                var qhuellas = ctx.Huellas.Where(i => i.idempleado == item.idempleado);
                foreach (var h in qhuellas)
                {
                    if (!res.ContainsKey(h.idempleado))
                        res.Add(h.idempleado, new List<byte[]>());

                    res[h.idempleado].Add(h.template);                    
                }
            }            
            return res;
        }
        public int tipo;
        public Empleado Login(string user, string pass)
        {
            var puestos = new int[] {
                (int)Common.Constants.Puesto.CJA,
                (int)Common.Constants.Puesto.ENC,
                (int)Common.Constants.Puesto.SUP
            };
            var ctx = new DataAccess.SirCoNominaDataContext();
            var item = ctx.Empleados.Where(i => i.usuariosistema.Trim() == user &&
                i.password.Trim() == pass
                && i.iddepto == (int)Common.Constants.Departamento.TDA
                && puestos.Contains(i.idpuesto)
                && i.estatus == "A").SingleOrDefault();
            //if (!int.TryParse(user, out id))
            //    return null;
            //if (pass != "123")
            //    return null;
            //var item = ctx.Empleados.Where(i => i.idempleado == id
            //    && i.iddepto == (int)Common.Constants.Departamento.TDA
            //    && puestos.Contains(i.idpuesto)
            //    && i.estatus == "A"
            //    && i.clave.Substring(0, 2) == sucursal).SingleOrDefault();
            if (item != null)
            {
                return new Empleado
                {
                    Id = item.idempleado,
                    ApellidoMaterno = item.apmaterno,
                    ApellidoPaterno = item.appaterno,
                    Nombre = item.nombre,
                    Usuario = item.usuariosistema,
                    Clave = item.clave,
                    Puesto = item.idpuesto,
                    Depto = item.iddepto
                };
            }
            return null;
        }
    }
}
