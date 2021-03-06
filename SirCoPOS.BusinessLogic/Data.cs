using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
            var item = ctx.Sucursales.Where(i => i.sucursal == sucursal).SingleOrDefault();
            if (item == null)
            {
                return null;
            }
            var res = new Common.Entities.Sucursal
            {
                Id = item.idsucursal,
                Clave = item.sucursal,
                Descripcion = item.descrip
            };
            return res;
        }
        public Empleado FindEmpleado(string user)
        {
            var ctx  = new DataAccess.SirCoNominaDataContext();
            var ctxC = new DataAccess.SirCoControlDataContext();
            DataAccess.SirCoNomina.Empleado emp = ctx.Empleados.Where(i => i.usuariosistema.Trim() == user && i.estatus == "A").SingleOrDefault();
            if (emp != null)
            {
                var suc = ctxC.Sucursales.Where(i => i.sucursal == emp.clave.Substring(0, 2)).SingleOrDefault();
                return new Empleado
                {
                    Id = emp.idempleado,
                    ApellidoMaterno = emp.apmaterno,
                    ApellidoPaterno = emp.appaterno,
                    Nombre = emp.nombre,
                    Usuario = emp.usuariosistema,
                    Clave = emp.clave,
                    Puesto = emp.idpuesto,
                    Depto = emp.iddepto,
                    Sucursal = suc.descrip
                };
            }
            return null;
        }
        public Empleado FindEmpleadoBono(int IdEmpleado)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var ctxC = new DataAccess.SirCoControlDataContext();
            DataAccess.SirCoNomina.Empleado emp = ctx.Empleados.Where(i => i.idempleado == IdEmpleado && i.estatus == "A").SingleOrDefault();
            if (emp != null)
            {
                var suc = ctxC.Sucursales.Where(i => i.sucursal == emp.clave.Substring(0, 2)).SingleOrDefault();
                return new Empleado
                {
                    Id = emp.idempleado,
                    ApellidoMaterno = emp.apmaterno,
                    ApellidoPaterno = emp.appaterno,
                    Nombre = emp.nombre,
                    Usuario = emp.usuariosistema,
                    Clave = emp.clave,
                    Puesto = emp.idpuesto,
                    Depto = emp.iddepto,
                    Sucursal = suc.descrip
                };
            }
            return null;
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
        public Empleado FindAuditorApertura(string sucursal, int idauditor, int idcajero)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var ctxpos = new DataAccess.SirCoPOSDataContext();
            var cajero = ctx.Empleados.Where(i => i.idempleado == idcajero
                && i.estatus == "A").Single();

            var auditor = ctx.Empleados.Where(i => i.idempleado == idauditor                
                && i.estatus == "A").SingleOrDefault();
            if (auditor != null)
            {                
                var asuc = auditor.clave.Substring(0, 2);
                var suc = cajero.clave.Substring(0, 2);
                decimal? disponible = null;

                var fondo = ctxpos.Fondos.Where(i => i.ResponsableId == idauditor && !i.FechaCierre.HasValue).SingleOrDefault();
                if ((int)Common.Constants.Departamento.ADM == auditor.iddepto)
                {
                    if (fondo != null)
                    {
                        throw new FondoAbiertoExcepcion(fondo.CajaSucursal, fondo.CajaNumero.Value);
                    }
                }
                else if (Common.Constants.Puestos.Gerentes.Contains(auditor.idpuesto))
                {
                    if (fondo == null)
                    {
                        throw new NoExisteFondoAbiertoExcepcion();
                    }
                    else disponible = fondo.Disponible;
                }
                else
                { 
                    return null;
                }

                string sucursalauditor = sucursal;
                if (auditor.iddepto == (int)Common.Constants.Departamento.TDA)
                {
                    sucursalauditor = asuc;
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
                    Disponible = disponible,
                    Depto = auditor.iddepto,
                    Sucursal = sucursalauditor
                };
                
            }
            return null;
        }
        public Empleado FindAuditorEntrega(string sucursal, int idauditor, int idcajero)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var ctxpos = new DataAccess.SirCoPOSDataContext();
            var cajero = ctx.Empleados.Where(i => i.idempleado == idcajero
                && i.estatus == "A").Single();

            var auditor = ctx.Empleados.Where(i => i.idempleado == idauditor
                && i.estatus == "A").SingleOrDefault();
            if (auditor != null)
            {
                var asuc = auditor.clave.Substring(0, 2);
                if (Common.Constants.Puestos.Gerentes.Contains(auditor.idpuesto))
                {
                    //Auditor debe tener fondo para recibir transferencia
                    var fondo = ctxpos.Fondos.Where(i => i.ResponsableId == auditor.idempleado && !i.FechaCierre.HasValue).SingleOrDefault();
                    if (fondo == null)
                        throw new NoExisteFondoAbiertoExcepcion();
                }
                else if (!Common.Constants.Puestos.Admin.Contains(auditor.idpuesto))
                    throw new ResponsableNoValidoExcepcion();

                string sucursalauditor = sucursal;
                if (auditor.iddepto == (int)Common.Constants.Departamento.TDA)
                    sucursalauditor = asuc;

                return new Empleado
                {
                    Id = auditor.idempleado,
                    ApellidoMaterno = auditor.apmaterno,
                    ApellidoPaterno = auditor.appaterno,
                    Nombre = auditor.nombre,
                    Usuario = auditor.usuariosistema,
                    Clave = auditor.clave,
                    Puesto = auditor.idpuesto,
                    Depto = auditor.iddepto,
                    Sucursal = sucursalauditor
                };
            }
            return null;
        }
        public Empleado FindAuditorTransferir(string sucursal, int idauditor, int idcajero)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var ctxpos = new DataAccess.SirCoPOSDataContext();
            var cajero = ctx.Empleados.Where(i => i.idempleado == idcajero
                && i.estatus == "A").Single();

            var auditor = ctx.Empleados.Where(i => i.idempleado == idauditor
                && i.estatus == "A").SingleOrDefault();
            if (auditor != null)
            {
                var asuc = auditor.clave.Substring(0, 2);
                var suc = cajero.clave.Substring(0, 2);
                if (Common.Constants.Puestos.Gerentes.Contains(auditor.idpuesto) )
                {
                    //Auditor NO debe tener fondo para recibir transferencia
                    var fondo = ctxpos.Fondos.Where(i => i.ResponsableId == idauditor && !i.FechaCierre.HasValue).SingleOrDefault();
                    if (fondo != null)
                        throw new FondoAbiertoExcepcion(fondo.CajaSucursal, fondo.CajaNumero.Value);
                }
                else
                    throw new ResponsableNoValidoExcepcion();

                return new Empleado
                {
                    Id = auditor.idempleado,
                    ApellidoMaterno = auditor.apmaterno,
                    ApellidoPaterno = auditor.appaterno,
                    Nombre = auditor.nombre,
                    Usuario = auditor.usuariosistema,
                    Clave = auditor.clave,
                    Puesto = auditor.idpuesto,
                    Sucursal = asuc,
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

        public string FindCiudad(int id)
        {
            var ctxc = new DataAccess.SirCoControlDataContext();
            var nombre = "";
            System.Collections.Generic.IEnumerable<SirCoPOS.DataAccess.SirCoControl.Ciudad> ciu = null;
            ciu = ctxc.Ciudades.Where(i => i.idciudad == id);

            foreach (var ci in ciu)
            {
                nombre = ci.ciudad;
                return nombre;
            }

            return "";
        }

        public string FindEstado(int id)
        {
            var ctxe = new DataAccess.SirCoControlDataContext();
            var nombre = "";
            System.Collections.Generic.IEnumerable<SirCoPOS.DataAccess.SirCoControl.Estado> est = null;

            est = ctxe.Estados.Where(i => i.idestado == id);

            foreach (var es in est)
            {
                nombre = es.estado;
                return nombre;
            }

            return "";
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
        public int TimeOut()
        {
            DataAccess.SirCoControlDataContext ctx = new DataAccess.SirCoControlDataContext();
            DataAccess.SirCoControl.Parametro item = ctx.Parametros.Where(i => i.sucursal == "99" && i.clave == "TIMEOUT").SingleOrDefault();
            if (item != null)
            {
                return Int32.Parse(item.valor);
            }
            return 60; // 60 segundos si el parámetro no existe
        }
        public Empleado Login(string sucursal, string user, string pass)
        {
            var puestos = new int[] {
                (int)Common.Constants.Puesto.CJA,
                (int)Common.Constants.Puesto.ENC,
                (int)Common.Constants.Puesto.SUP
            };
            var ctx = new DataAccess.SirCoNominaDataContext();

            //Busqueda del Usuario
            DataAccess.SirCoNomina.Empleado item = ctx.Empleados.Where(i => i.usuariosistema == user && i.password.Trim() == pass && i.estatus == "A").SingleOrDefault();
            if (item == null)
            {
                return null;
            }
            // Empleado no es del depto SIS ni tampoco del depto ADM
            if (item.iddepto != (int)Common.Constants.Departamento.SIS && item.iddepto != (int)Common.Constants.Departamento.ADM)
            { 
                item = ctx.Empleados.Where(i => i.usuariosistema == user
                && i.iddepto == (int)Common.Constants.Departamento.TDA
                && puestos.Contains(i.idpuesto)
                && i.estatus == "A"
                && i.clave.Substring(0, 2) == sucursal).SingleOrDefault();
            }
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
        public Empleado AuditorPassword(int id, string pass)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var emp = ctx.Empleados.Where(i => i.idempleado == id && i.password == pass && i.estatus == "A").SingleOrDefault();
            if ( emp != null )
            {
                return new Empleado
                {
                    Id = emp.idempleado,
                    ApellidoMaterno = emp.apmaterno,
                    ApellidoPaterno = emp.appaterno,
                    Nombre = emp.nombre,
                    Usuario = emp.usuariosistema,
                    Clave = emp.clave,
                    Puesto = emp.idpuesto,
                    Depto = emp.iddepto,
                };
            }
            return null;
        }

        public  IEnumerable<Common.Entities.PorcentajeFormaPago> GetPorcentajeFPago (string sucursal, string devolucion)
        {
            var ctx = new DataAccess.SirCoPVDataContext();

            IEnumerable<Common.Entities.PorcentajeFormaPago> porcentajes = ctx.GetPorcentajeFPago(sucursal, devolucion);

            return porcentajes;

        }
        public bool Acceso(int idempleado, string sucursal, bool acceso)
        {
            var now = Helpers.Common.GetNow();
            var fechaVacia = DateTime.Parse(Formats.DATE_EMPTY);
            var ctx = new DataAccess.SirCoPOSDataContext();
            var ctxn = new DataAccess.SirCoNominaDataContext();
            if (acceso)
            {
                var adentro = ctx.UsuarioAccesos.Where(i => i.idempleado== idempleado && i.entrada != fechaVacia && i.salida == fechaVacia).Any();
                if (adentro)
                    return false;

                var Empleado = ctxn.Empleados.Where(i => i.idempleado == idempleado).SingleOrDefault();

                    IPHostEntry host = Dns.GetHostEntry(LocalIPAddress());
                string hostName = host.HostName;

                var item = new DataAccess.SirCoPOS.UsuarioAcceso
                {
                    idempleado = Empleado.idempleado,
                    nombre = Empleado.nombre + " " + Empleado.appaterno + " " + Empleado.apmaterno,
                    iddepto = Empleado.iddepto,
                    idpuesto = Empleado.idpuesto,
                    sucursal = sucursal,
                    entrada = now,
                    nombrepc = hostName,
                    salida = fechaVacia
                };
                ctx.UsuarioAccesos.Add(item);
                if (ctx.SaveChanges() != 1)
                    return false;
            }
            else
            {
                var adentro = ctx.UsuarioAccesos.Where(i => i.idempleado== idempleado && i.entrada!= fechaVacia && i.salida == fechaVacia).SingleOrDefault();
                if (adentro == null)
                    return false;

                adentro.salida = now;
                if (ctx.SaveChanges() != 1)
                {
                    return false;
                }
                    
            }
            return true;
        }

        private IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
        public string RegistraOperacion(OperacionTarjeta request, Guid gid)
        {
            var ctxpv = new DataAccess.SirCoPVDataContext();
            DateTime fecha = DateTime.Parse(Formats.DATE_EMPTY);

            var now =  Helpers.Common.GetNow();
            if (request.data.transDate != null)
            {
                int timeZonePos = request.data.transDate.LastIndexOf('.') + 1;
                string tz = request.data.transDate.Substring(timeZonePos);
                var dateString = request.data.transDate.Substring(0, request.data.transDate.Length - tz.Length-1);

                if (Helpers.Common.s_timeZoneOffsets.ContainsKey(tz))
                {
                    dateString += Helpers.Common.s_timeZoneOffsets[tz];
                    fecha = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:sszzz", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    fecha = Convert.ToDateTime(dateString);
                }
            }
            DateTime fechaTran = fecha;
            
            decimal Amount = 0;
            decimal TipAmount = 0;
            decimal TipLessAmount = 0;
            if (request.data.tipAmount != null)
                TipAmount = Convert.ToDecimal(request.data.tipAmount);
            if (request.data.tipLessAmount != null)
                TipLessAmount = Convert.ToDecimal(request.data.tipLessAmount);
            if (request.data.amount != null)
                Amount = Convert.ToDecimal(request.data.amount);

            var item = new DataAccess.SirCoPV.OperacionTarjeta
            {
                affiliation = request.data.affiliation,
                applicationLabel = request.data.applicationLabel,
                arqc = request.data.arqc,
                aid = request.data.aid,
                amount = Amount,
                authCode = request.data.authCode,
                bankName = request.data.bankName,
                cardExpDate = request.data.cardExpDate,
                cardType = request.data.cardType,
                cardTypeName = request.data.cardTypeName,
                cityName = request.data.cityName,
                responseCode = request.data.responseCode,
                folioNumber = request.data.folioNumber,
                hasPin = request.data.hasPin,
                hexSign = request.data.hexSign,
                isQps = request.data.isQPS,
                message = request.data.message,
                moduleCharge = request.data.moduleCharge,
                moduleLote = request.data.moduleLote,
                customerName = request.data.customerName,
                terminalId = request.data.terminalId,
                orderId = request.data.orderId,
                preAuth = request.data.preAuth,
                preStatus = request.data.preStatus,
                promotion = request.data.promotion,
                rePrintDate = request.data.rePrintDate,
                rePrintMark = request.data.rePrintMark,
                rePrintModule = request.data.reprintModule,
                cardNumber = request.data.cardNumber,
                storeId = request.data.storeId,
                storeName = request.data.storeName,
                streetName = request.data.streetName,
                ticketDate = request.data.ticketDate,
                tipAmount = TipAmount,
                tipLessAmount = TipLessAmount,
                transDate = fechaTran,
                transType = request.data.transType,
                transactionCertificate = request.data.transactionCertificate,
                guid= gid,
                fum = now
            };
            ctxpv.OperacionesTarjeta.Add( item );
            ctxpv.SaveChanges();
            return request.data.responseCode;
        }
    }
}
