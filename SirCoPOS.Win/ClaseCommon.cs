using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.Common.Entities;

namespace SirCoPOS.Win
{
    public class ClaseCommon
    {
        private readonly BusinessLogic.Data _helpers;
        public ClaseCommon()
        {
            _helpers = new BusinessLogic.Data();
        }
        public Empleado Login(string sucursal, string user, string pass)
        {
            return _helpers.Login(user, pass);
        }
        public Empleado FindVendedor(int id)
        {
            var ctx = new SirCoPOS.DataAccess.SirCoNominaDataContext();
            var item = ctx.Empleados.Where(i => i.idempleado == id
                && i.iddepto == (int)Common.Constants.Departamento.TDA
                && i.idpuesto == (int)Common.Constants.Puesto.VEN
                && i.estatus == "A"
                ).SingleOrDefault();
            if (item != null)
            {
                return new Empleado
                {
                    Id = item.idempleado,
                    Clave = item.clave,
                    ApellidoMaterno = item.apmaterno,
                    ApellidoPaterno = item.appaterno,
                    Nombre = item.nombre,
                    Usuario = item.usuariosistema
                };
            }
            return null;
        }
        public Empleado FindCajero(string user)
        {
            return _helpers.FindCajero(user);
        }
        public Sucursal FindSucursal(string sucursal)
        {
            return _helpers.FindSucursal(sucursal);
        }
        public Empleado CheckFingerLogin(string sucursal, byte[] huella)
        {
            var puestos = new int[] {
                (int)Common.Constants.Puesto.CJA,
                (int)Common.Constants.Puesto.ENC,
                (int)Common.Constants.Puesto.SUP
            };
            var proxy = new System.ServiceModel.ChannelFactory<Common.ServiceContracts.ILocalService>("*").CreateChannel();
            var ctx = new SirCoPOS.DataAccess.SirCoNominaDataContext();
            //var helper = new BusinessLogic.Helpers.FingerHelper();
            var q = ctx.Empleados.Where(i => i.HuellasPOS.Any()
                && i.iddepto == (int)Common.Constants.Departamento.TDA
                && puestos.Contains(i.idpuesto)
                && i.estatus == "A"
                && i.clave.Substring(0, 2) == sucursal)
                .OrderByDescending(i => i.idpuesto);
        SirCoPOS.DataAccess.SirCoNomina.Empleado item = null;
            var valid = false;
            foreach (var empleado in q)
            {
                foreach (var dedo in empleado.HuellasPOS)
                {
                    //if (helper.Verify(huella, dedo.template))
                    if (proxy.Match(huella, dedo.template))
                    {
                        item = empleado;
                        valid = true;
                        break;
                    }
                }
                if (valid)
                    break;
            }
            if (item != null)
            {
                return new Empleado
                {
                    Id = item.idempleado,
                    Clave = item.clave,
                    ApellidoMaterno = item.apmaterno,
                    ApellidoPaterno = item.appaterno,
                    Nombre = item.nombre,
                    Usuario = item.usuariosistema
                };
            }
            return null;
        }
        public Empleado CheckFingerAdmin(byte[] huella)
        {
            var puestos = new int[] {
                //(int)Common.Constants.Puesto.CJA,
                //,JFA = 1 //JEFE DE ADMINISTRACIÓN
                //,GEA = 10 //GERENTE ADMINISTRATIVO
                //,AUD = 13 //AUDITOR
                //,EGR = 17 //EGRESOS
                //,AXC = 35 //AUXILIAR CONTABLE
                //,AUA = 52 //AUXILIAR ADMINISTRACION

                //,MEN = 14 //MENSAJERO
            };
            var proxy = new System.ServiceModel.ChannelFactory<Common.ServiceContracts.ILocalService>("*").CreateChannel();
            var ctx = new SirCoPOS.DataAccess.SirCoNominaDataContext();
            //var helper = new BusinessLogic.Helpers.FingerHelper();
            var q = ctx.Empleados.Where(i => i.Huellas.Any()
                && i.iddepto == (int)Common.Constants.Departamento.ADM
                //&& puestos.Contains(i.idpuesto)
                && i.estatus == "A");
            SirCoPOS.DataAccess.SirCoNomina.Empleado item = null;
            var huellas = new Dictionary<int, byte[]>();
            var empleados = new Dictionary<int, SirCoPOS.DataAccess.SirCoNomina.Empleado>();
            foreach (var empleado in q)
            {
                foreach (var dedo in empleado.Huellas)
                {
                    empleados.Add(dedo.iddedo, empleado);
                    huellas.Add(dedo.iddedo, dedo.template);
                }
            }
            var did = proxy.Find(huella, huellas);
            if (did.HasValue)
                item = empleados[did.Value];
            if (item != null)
            {
                return new Empleado
                {
                    Id = item.idempleado,
                    Clave = item.clave,
                    ApellidoMaterno = item.apmaterno,
                    ApellidoPaterno = item.appaterno,
                    Nombre = item.nombre,
                    Usuario = item.usuariosistema
                };
            }
            return null;
        }
        public byte[] GetFingerEmpleado(int idempleado)
        {
            var ctx = new SirCoPOS.DataAccess.SirCoNominaDataContext();
            var item = ctx.Empleados.Where(i => i.Huellas.Any()
                && i.idempleado == idempleado
                && i.estatus == "A").SingleOrDefault();
            if (item != null)
            {
                var huella = item.Huellas.First();
                return huella.template;
            }
            return null;
        }
    }
}
