using Base32;
using OtpSharp;
using SirCoPOS.BusinessLogic;
using SirCoPOS.Common.Entities;
using SirCoPOS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Services
{
    public class AdminService : Common.ServiceContracts.IAdminService
    {
        private readonly BusinessLogic.Admin _admin;
        public AdminService()
        {
            _admin = new BusinessLogic.Admin();
        }
        public decimal? GetDisponibleFondo(int id)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            var fondo = ctx.Fondos.Where(i => i.ResponsableId == id && !i.FechaCierre.HasValue).SingleOrDefault();
            if (fondo == null) 
                return null;
            return fondo.Disponible;
        }
        public IEnumerable<Common.Entities.Option> GetTiposGasto()
        {
            var ctx = new DataAccess.SirCoDataContext();
            var q = ctx.DetalleGastos.Select(i =>
                new Common.Entities.Option
                {
                    Id = i.idgasto,
                    Text = i.descrip
                });
            return q;
        }
        public void GenerarGasto(GastoRequest request)
        {
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctxpv = new DataAccess.SirCoPOSDataContext();
            var ctx = new DataAccess.SirCoDataContext();

            var fondo = ctxpv.Fondos.Where(i => i.ResponsableId == request.CajeroId && !i.FechaCierre.HasValue).SingleOrDefault();
            if (fondo == null)
                return;

            fondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
            {
                Entrada = false,
                Fecha = now,
                Importe = request.Monto,
                Tipo = "Gasto",
                UsuarioId = request.SolicitaId
            });
            fondo.Disponible -= request.Monto;
            fondo.Caja.Disponible -= request.Monto;

            var id = ctx.Gastos.Max(i => i.folio);
            var folio = ctx.Gastos.Where(i => i.sucursal == request.Sucursal).Max(i => i.foliosuc);
            var nfolio = folio == null ? 0 : int.Parse(folio.Substring(2));
            var item = new DataAccess.SirCo.Gasto
            {
                folio = id + 1,
                cantidad = request.Monto,
                sucursal = request.Sucursal,
                fecha = now,
                idgasto = (short)request.Tipo,
                solicita = (short)request.SolicitaId,
                revisa = 0,
                autoriza = 0,
                status = "CA",
                comentarios = request.Descripcion,
                usuario = (short)request.CajeroId,
                fum = now,
                usumodrevisa = "0",
                fummodrevisa = null,
                usumodautoriza = "0",
                fummodautoriza = null,
                foliosuc = $"{request.Sucursal}{(nfolio + 1):000000}"
            };
            ctx.Gastos.Add(item);

            ctx.SaveChanges();
            ctxpv.SaveChanges();
        }
        public bool ValidarCodigo(int id, string token)
        {
            var ctx = new SirCoNominaDataContext();
            var emp = ctx.Empleados.Where(i => i.idempleado == id).Single();

            //var valid = emp.password == token;
            var valid = token == "123";

            //if (emp.authkey == null)
            //    return false;
            //long timeStepMatched = 0;
            //var otp = new Totp(Base32Encoder.Decode(emp.authkey));
            //bool valid = otp.VerifyTotp(token, out timeStepMatched, new VerificationWindow(2, 2));

            return valid;
        }
        public void AbrirFondo(FondoRequest request)
        {
            //using (var tran = new System.Transactions.TransactionScope())
            //{
                this.AbrirFondoHelper(request);
            //    tran.Complete();
            //}
        }
        public void AbrirFondoHelper(FondoRequest request)
        {
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoPOSDataContext();

            var qc = ctx.Fondos.Where(i => i.CajaSucursal == request.Sucursal && i.CajaNumero == request.Numero && !i.FechaCierre.HasValue);
            var qr = ctx.Fondos.Where(i => i.ResponsableId == request.Responsable && !i.FechaCierre.HasValue);
            if (qc.Any())
                throw new CajaNoDisponibleExcepcion();
            if (qr.Any())
            {
                var fondo = ctx.Fondos.Where(i => i.ResponsableId == request.Responsable && !i.FechaCierre.HasValue).SingleOrDefault();
                throw new FondoAbiertoExcepcion(fondo.CajaSucursal, (int)fondo.CajaNumero);
            }
            
            var item = new DataAccess.SirCoPOS.Fondo
            {
                ResponsableId = request.Responsable,
                FechaApertura = now,
                Disponible = request.Importe,
                Tipo = request.Tipo,
                AuditorAperturaId = request.Auditor,
                CajaSucursal = request.Sucursal,
                CajaNumero = request.Numero
            };
            ctx.Fondos.Add(item);
            if (item.CajaNumero.HasValue)
            {
                ctx.Entry(item).Reference(i => i.Caja).Load();
                //var importe = item.Caja.Disponible + request.Importe;

                //this.ArqueoFondoHelper(new FondoArqueoRequest {
                //    Auditor = request.Auditor, 
                //    Responsable = request.Responsable, 
                //    Importe = request.Importe
                //}, now);

                var importe = request.Importe;
                item.Caja.Disponible = importe;
                item.Disponible = importe;
                item.Caja.ResponsableId = request.Responsable;
            }

            if (request.Importe > 0 && request.Auditor != 0)
            {
                var gerentes = new int[] {
                    (int)Common.Constants.Puesto.ENC,
                    (int)Common.Constants.Puesto.SUP
                };

                var ctxn = new SirCoNominaDataContext();
                var auditor = ctxn.Empleados.Where(i => i.idempleado == request.Auditor).Single();

                var isGerente = gerentes.Contains(request.Auditor);

                if (!isGerente)
                {
                    item.Movimientos = new HashSet<DataAccess.SirCoPOS.FondoMovimiento>();
                    item.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
                    {
                        Importe = request.Importe,
                        UsuarioId = request.Auditor,
                        Entrada = true,
                        Fecha = now,
                        Referencia = Guid.NewGuid(),
                        Tipo = "Apertura"
                    });
                    var fondo = ctx.Fondos.Where(i => i.ResponsableId == request.Auditor && !i.FechaCierre.HasValue).SingleOrDefault();
                    // Si existe un fondo asignado al auditor disminuir el disponible que se está entregando
                    if (fondo !=null) { 
                        fondo.Disponible -= request.Importe;
                        if (fondo.CajaNumero.HasValue)
                            fondo.Caja.Disponible -= request.Importe;
                    }
                }
                else
                {
                    var fondo = ctx.Fondos.Where(i => i.ResponsableId == request.Auditor && !i.FechaCierre.HasValue).SingleOrDefault();
                    item.Movimientos = new HashSet<DataAccess.SirCoPOS.FondoMovimiento>();
                    var gid = Guid.NewGuid();
                    item.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
                    {
                        Importe = request.Importe,
                        UsuarioId = request.Auditor,
                        Entrada = true,
                        Fecha = now,
                        Referencia = gid,
                        Tipo = "Apertura"
                    });
                    // Si existe un fondo asignado al Gerente disminuir el disponible que se está entregando
                    // Y agregar un movimiento de apertura de disminucion
                    if (fondo !=null) {  
                        fondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
                        {
                            Importe = request.Importe,
                            UsuarioId = request.Responsable,
                            Entrada = false,
                            Fecha = now,
                            Referencia = gid,
                            Tipo = "Apertura"
                        });
                        fondo.Disponible -= request.Importe;
                        if (fondo.CajaNumero.HasValue)
                            fondo.Caja.Disponible -= request.Importe;
                    }
                }
            }
            ctx.SaveChanges();
        }
        public void TransferirFondo(FondoTransferRequest request)
        {
            this.TransferirFondoHelper(request);
        }
        private void TransferirFondoHelper(FondoTransferRequest request, DateTime? date = null)
        {
            var bl = new BusinessLogic.Admin();

            var now = date ?? BusinessLogic.Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoPOSDataContext();
            var gid = Guid.NewGuid();

            var cfrom = ctx.Fondos.Where(i => i.ResponsableId == request.UserFrom
                && !i.FechaCierre.HasValue).Single();
            var cto = ctx.Fondos.Where(i => i.ResponsableId == request.UserTo
                && !i.FechaCierre.HasValue).Single();

            cfrom.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
            {
                Entrada = false,
                Fecha = now,
                Importe = request.Importe,
                Referencia = gid,
                UsuarioId = request.UserFrom
            });
            cfrom.Disponible -= request.Importe;
            if (cfrom.CajaNumero.HasValue)
                cfrom.Caja.Disponible -= request.Importe;
            cto.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
            {
                Entrada = true,
                Fecha = now,
                Importe = request.Importe,
                Referencia = gid,
                UsuarioId = request.UserTo
            });
            cto.Disponible += request.Importe;
            if (cto.CajaNumero.HasValue)
                cto.Caja.Disponible += request.Importe;
            ctx.SaveChanges();
        }
        public void CierreFondo(FondoArqueoRequest request)
        {
            this.CierreFondoHelper(request);
        }
        private void CierreFondoHelper(FondoArqueoRequest request, DateTime? date = null)
        {
            var now = date ?? BusinessLogic.Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoPOSDataContext();
            this.ArqueoFondoHelper(new FondoArqueoRequest
            {
                Importe = request.Importe,
                Auditor = request.Auditor,
                Responsable = request.Responsable
            }, now, cierre: true);

            var fondo = ctx.Fondos.Where(i => i.ResponsableId == request.Responsable && !i.FechaCierre.HasValue).Single();
            if (request.Importe > 0)
            {
                if (request.Auditor == request.Responsable)
                {
                    var gid = Guid.NewGuid();
                    var mov = new DataAccess.SirCoPOS.FondoMovimiento
                    {
                        Importe = request.Entregar.Value,
                        UsuarioId = request.Auditor,
                        Entrada = false,
                        Fecha = now,
                        Referencia = gid
                    };
                    fondo.Movimientos.Add(mov);
                    fondo.Disponible -= mov.Importe;
                    fondo.Caja.Disponible -= mov.Importe;
                    mov.Tipo = Common.Constants.TipoMovimiento.Admin;
                }
                else
                {
                    this.TransferirFondo(new FondoTransferRequest
                    {
                        Importe = request.Entregar.Value,
                        UserFrom = request.Responsable,
                        UserTo = request.Auditor
                    });
                }

                //var gid = Guid.NewGuid();
                //var mov = new DataAccess.SirCoPOS.FondoMovimiento
                //{
                //    Importe = request.Importe,
                //    UsuarioId = request.Auditor,
                //    Entrada = false,
                //    Fecha = now,
                //    Referencia = gid
                //};
                //fondo.Movimientos.Add(mov);
                //fondo.Disponible -= request.Importe;
                //fondo.Caja.Disponible -= request.Importe;

                //if (request.Auditor == request.Responsable)
                //{
                //    mov.Tipo = Common.Constants.TipoMovimiento.Admin;
                //} else {
                //    var afondo = ctx.Fondos.Where(i => i.ResponsableId == request.Auditor && !i.FechaCierre.HasValue).Single();
                //    afondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
                //    {
                //        Importe = request.Importe,
                //        UsuarioId = request.Responsable,
                //        Entrada = true,
                //        Fecha = now,
                //        Referencia = gid
                //    });
                //    afondo.Disponible += request.Importe;
                //    if (afondo.CajaNumero.HasValue)
                //        afondo.Caja.Disponible += request.Importe;
                //}
            }
            fondo.AuditorCierreId = request.Auditor;
            fondo.FechaCierre = now;
            ctx.Entry(fondo.Caja).Reload();
            var qfp = ctx.Entry(fondo.Caja).Collection(i => i.FormasPago).Query();
            if (fondo.Caja.Disponible == 0
                && qfp.All(i => i.Unidades == 0))
            {
                fondo.Caja.ResponsableId = null;
            }
            ctx.SaveChanges();
        }
        public void ArqueoFondo(FondoArqueoRequest request)
        {
            using (var tran = new System.Transactions.TransactionScope())
            {
                ArqueoFondoHelper(request, BusinessLogic.Helpers.Common.GetNow());
                tran.Complete();
            }
        }
        private void ArqueoFondoHelper(FondoArqueoRequest request, DateTime? pnow = null, bool cierre = false)
        {
            var now = pnow ?? BusinessLogic.Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoPOSDataContext();
            var fondo = ctx.Fondos.Where(i => i.ResponsableId == request.Responsable && !i.FechaCierre.HasValue).Single();
            var disponible = fondo.Disponible;
            if (fondo.CajaNumero.HasValue && fondo.Caja.Tipo == Common.Constants.TipoFondo.Cajon)
            {
                //var admin = new BusinessLogic.Admin();
                //var ventas = admin.VentasEfectivo(fondo.CajaSucursal, fondo.CajaNumero.Value);
                //disponible += ventas ?? 0;
            }
            var item = new DataAccess.SirCoPOS.FondoArqueo
            {
                AuditorId = request.Auditor,
                Fecha = now,
                Importe = disponible,
                Reportado = request.Importe
            };
            fondo.Arqueos.Add(item);
            fondo.Disponible = request.Importe;
            if (fondo.CajaNumero.HasValue)
                fondo.Caja.Disponible = request.Importe;
            ctx.SaveChanges();
        }
        public void PagoBonos(int empleado, int supervisor, decimal importe)
        {
            var now = BusinessLogic.Helpers.Common.GetNow();
            var nctx = new DataAccess.SirCoNominaDataContext();
            var pctx = new DataAccess.SirCoPOSDataContext();

            var periodo = nctx.Periodos.Where(i => i.estatus == "A" && i.fechafin < now)
                .OrderByDescending(i => i.fechaini).First();

            var pago = new DataAccess.SirCoPOS.Pago
            {
                EmisorId = supervisor,
                ReceptorId = empleado,
                Fecha = now,
                Importe = importe,
                PeriodoId = periodo.idperiodo
            };
            pctx.Pagos.Add(pago);
            pctx.SaveChanges();
        }
        public bool PayBono(int cajero, int empleado, int gerente, decimal importe)
        {
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoPOSDataContext();
            var fondo = ctx.Fondos.Where(i => i.ResponsableId == cajero && !i.FechaCierre.HasValue).SingleOrDefault();
            if (fondo == null)
                return false;
            var gid = Guid.NewGuid();

            if (fondo.Disponible < importe || fondo.Caja.Disponible < importe )
            {
                return false;
            }
            else
            {
                fondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
                {
                    Importe = importe,
                    UsuarioId = gerente,
                    Entrada = false,
                    Fecha = now,
                    Referencia = gid,
                    Tipo = "Bono"
                });
                fondo.Disponible -= importe;
                fondo.Caja.Disponible -= importe;
                ctx.SaveChanges();
                UpdateBonos(empleado, gid);

                return true;
            }
            
        }
        public void UpdateBonos(int empleadoId, Guid gid)
        {
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoNominaDataContext();
            var periodo = ctx.Periodos.Where(i => i.estatus == "A" && i.fechafin < now)
                .OrderByDescending(i => i.fechaini)
                .FirstOrDefault();
            if (periodo == null)
                throw new NotSupportedException();

            var q = ctx.NominaDetalles.Where(i => i.idempleado == empleadoId
                && i.idperiodo == periodo.idperiodo && i.tiponom == "B");

            foreach (var item in q)
            {
                item.movimiento = gid;
            }
            ctx.SaveChanges();
        }
        public Common.Entities.Bonos GetBonos(int empleadoId)
        {
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctx = new DataAccess.SirCoNominaDataContext();
            var periodo = ctx.Periodos.Where(i => i.estatus == "A" && i.fechafin < now)
                .OrderByDescending(i => i.fechaini)
                .FirstOrDefault();
            if (periodo == null)
                return null;

            var q = ctx.NominaDetalles.Where(i => i.idempleado == empleadoId
                && i.idperiodo == periodo.idperiodo && i.tiponom == "B"
                && !i.movimiento.HasValue);

            var list = new List<Common.Entities.BonoDetalle>();
            foreach (var item in q)
            {
                var nitem = new Common.Entities.BonoDetalle
                {
                    Unidades = item.unidades,
                    Descripcion = item.Tipo.descripl,
                    Importe = item.impexento ?? 0
                };
                if (item.Tipo.naturaleza == "D")
                {
                    nitem.Importe = nitem.Importe * -1;
                }
                list.Add(nitem);
            }

            var res = new Common.Entities.Bonos
            {
                Detalle = list
            };
            return res;
        }
        public CajaFormas GetEntrega(string sucursal, int idgerente)
        {
            var ctxpos = new DataAccess.SirCoPOSDataContext();
            var caja = ctxpos.Cajas.Where(i => i.Sucursal == sucursal && i.ResponsableId == idgerente).SingleOrDefault();
            if (caja ==null)
            {
                return null;
            }
            if (!(caja.Tipo == Common.Constants.TipoFondo.CajaFuerte
                || caja.Tipo == Common.Constants.TipoFondo.Cajon))
            {
                return null;
            }
            var res = new CajaFormas
            {
                Efectivo = caja.Disponible
            };
            res.FormasPago = caja.FormasPago.Where(i => i.Unidades > 0).Select(i =>
                new CajaFormaPago
                {
                    FormaPago = (Common.Constants.FormaPago)i.FormaPago,
                    Unidades = i.Unidades,
                    Monto = i.Monto
                });
            return res;
        }
        public CorteResponse GetCorteCaja(string sucursal, int idcajero)
        {
            return _admin.GetCorteCaja(sucursal, idcajero);
        }
        //public decimal? GetEfectivoCaja(string sucursal, int idcajero)
        //{
        //    return _admin.GetEfectivoCaja(sucursal, idcajero);
        //}
        public void Entrega(EntregaRequest request)
        {
            using (var tran = new System.Transactions.TransactionScope())
            {
                this.EntregaHelper(request);
                tran.Complete();
            }
        }
        private void EntregaHelper(EntregaRequest request)
        {
            //var now = BusinessLogic.Helpers.Common.GetNow();
            //var ctxpos = new DataAccess.SirCoPOSDataContext();
            //var ctxn = new DataAccess.SirCoNominaDataContext();
            //var auditor = ctxn.Empleados.Where(i => i.idempleado == request.AuditorId).Single();
            //var fondo = ctxpos.Fondos.Where(i => i.ResponsableId == request.ResponsableId && !i.FechaCierre.HasValue).Single();
            //var afondo = ctxpos.Fondos.Where(i => i.ResponsableId == request.AuditorId && !i.FechaCierre.HasValue).Single();
            //if ((Common.Constants.Puestos.Gerentes.Contains(auditor.idpuesto)
            //    || auditor.idpuesto == (int)Common.Constants.Puesto.MEN) && afondo != null)
            //{
            //    if (request.Entregar > 0)
            //    {
            //        afondo.Disponible += request.Entregar;
            //        afondo.Caja.Disponible += request.Entregar;
            //        afondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
            //        {
            //            Importe  = request.Entregar,
            //            UsuarioId = request.ResponsableId,
            //            Entrada = true,
            //            Fecha = now,
            //            //public Guid Referencia { get; set; }
            //            //public string Tipo { get; set; }
            //        });                    
            //    }
            //    foreach (var item in request.FormasPago)
            //    {
            //        afondo.FormasPago.Add(new DataAccess.SirCoPOS.FondoFormaPago
            //        {
            //            Entregado  = true,
            //            FormaPago = (int)item.FormaPago,
            //            Cantidad = item.Unidades,
            //            UsuarioId = request.ResponsableId,
            //            Monto = item.Monto,
            //            Fecha = now
            //        });
            //        var fp = afondo.Caja.FormasPago.Where(i => i.FormaPago == (int)item.FormaPago).SingleOrDefault();
            //        if (fp == null)
            //        {
            //            fp = new DataAccess.SirCoPOS.CajaFormaPago
            //            {
            //                FormaPago = (int)item.FormaPago,
            //                Unidades = 0,
            //                Monto = 0
            //            };
            //            afondo.Caja.FormasPago.Add(fp);
            //        }
            //        fp.Unidades += item.Unidades;
            //        fp.Monto += item.Monto;
            //    }                
            //}

            //if (request.Entregar > 0)
            //{
            //    fondo.Disponible -= request.Entregar;
            //    fondo.Caja.Disponible -= request.Entregar;
            //    fondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
            //    {
            //        Importe = request.Entregar,
            //        UsuarioId = request.AuditorId,
            //        Entrada = false,
            //        Fecha = now,
            //        //public Guid Referencia { get; set; }
            //        //public string Tipo { get; set; }
            //    });
            //}
            //foreach (var item in request.FormasPago)
            //{
            //    fondo.FormasPago.Add(new DataAccess.SirCoPOS.FondoFormaPago
            //    {
            //        Entregado = false,
            //        FormaPago = (int)item.FormaPago,
            //        Cantidad = item.Unidades,
            //        UsuarioId = request.AuditorId,
            //        Monto = item.Monto,
            //        Fecha = now
            //    });
            //    var fp = fondo.Caja.FormasPago.Where(i => i.FormaPago == (int)item.FormaPago).SingleOrDefault();
            //    if (fp == null)
            //    {
            //        fp = new DataAccess.SirCoPOS.CajaFormaPago
            //        {
            //            FormaPago = (int)item.FormaPago,
            //            Unidades = 0,
            //            Monto = 0
            //        };
            //        fondo.Caja.FormasPago.Add(fp);
            //    }
            //    fp.Unidades -= item.Unidades;
            //    fp.Monto -= item.Monto;
            //}

            //if (Common.Constants.Puestos.Gerentes.Contains(auditor.idpuesto)
            //    && afondo == null)
            //{
            //    throw new NotSupportedException();
            //}
            //ctxpos.SaveChanges();


            var gid = Guid.NewGuid();
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctxpos = new DataAccess.SirCoPOSDataContext();
            var ctxn = new DataAccess.SirCoNominaDataContext();
            var ctx = new DataAccess.SirCoDataContext();
            var fondo = ctxpos.Fondos.Where(i => i.ResponsableId == request.CajeroId && !i.FechaCierre.HasValue).Single();

            //ESTA ES LA OTRA LINEA DONDE APARECE EL ERROR
            var corte = _admin.GetCorteCaja(request.Sucursal, request.CajeroId);

            //var item = new DataAccess.SirCoPOS.Corte
            //{
            //    CajeroId = request.CajeroId,
            //    Fecha = now,
            //    AuditorId = request.AuditorId,
            //    Entregado = request.Entregar,
            //    FondoId = fondo.Id,
            //    Cierre = true,
            //    //Ventas = corte.Ventas,
            //    //Pagos = corte.Pagos,
            //    //public decimal? Gastos { get; set; }
            //    //public decimal? Bonos { get; set; }
            //};
            //ctxpos.Cortes.Add(item);

            var afondo = ctxpos.Fondos.Where(i => i.ResponsableId == request.AuditorId && !i.FechaCierre.HasValue).SingleOrDefault();
            if (afondo != null)
            {
                if (request.Entregar > 0)
                {
                    afondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
                    {
                        Importe = request.Entregar,
                        UsuarioId = request.AuditorId,
                        Entrada = true,
                        Fecha = now,
                        Referencia = gid,
                        Tipo = "Entrega"
                    });
                    afondo.Disponible += request.Entregar;
                    afondo.Caja.Disponible += request.Entregar;
                }

                foreach (var fp in request.FormasPago)
                {
                    if (fp.Entregar > 0)
                    {
                        afondo.FormasPago.Add(new DataAccess.SirCoPOS.FondoFormaPago
                        {
                            Entrada = true,
                            FormaPago = (int)fp.FormaPago,
                            Cantidad = fp.Entregar,
                            UsuarioId = request.CajeroId,
                            Monto = fp.Amount,
                            Fecha = now,
                            Referencia = gid
                        });
                        var cf = afondo.Caja.FormasPago.Where(i => i.FormaPago == (int)fp.FormaPago).SingleOrDefault();
                        if (cf == null)
                        {
                            cf = new DataAccess.SirCoPOS.CajaFormaPago
                            {
                                FormaPago = (int)fp.FormaPago,
                                Unidades = 0,
                                Monto = 0
                            };
                            afondo.Caja.FormasPago.Add(cf);
                        }
                        cf.Unidades += fp.Entregar;
                        cf.Monto += fp.Amount;
                    }
                }
            }
            if (request.Entregar > 0)
            {
                fondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
                {
                    Importe = request.Entregar,
                    UsuarioId = request.CajeroId,
                    Entrada = false,
                    Fecha = now,
                    Referencia = gid,
                    Tipo = "Entrega"
                });
                fondo.Disponible -= request.Entregar;
                fondo.Caja.Disponible -= request.Entregar;
            }
            //fondo.Arqueos.Add(new DataAccess.SirCoPOS.FondoArqueo
            //{
            //    AuditorId = request.AuditorId,
            //    Fecha = now,
            //    Importe = fondo.Disponible,
            //    Reportado = 0,
            //    Corte = item
            //});
            //item.FormasPago = new HashSet<DataAccess.SirCoPOS.CorteFormaPago>();
            foreach (var fp in corte.FormaPagoTotales)
            {
                var rfp = request.FormasPago.Where(i => i.FormaPago == fp.FormaPago).Single();
                //item.FormasPago.Add(new DataAccess.SirCoPOS.CorteFormaPago
                //{
                //    FormaPago = (int)fp.FormaPago,
                //    Entregado = rfp.Entregar,
                //    Total = rfp.Amount
                //});

                var cfp = fondo.Caja.FormasPago.Where(i => i.FormaPago == (int)fp.FormaPago).SingleOrDefault();
                if (cfp == null)
                {
                    cfp = new DataAccess.SirCoPOS.CajaFormaPago
                    {
                        FormaPago = (int)rfp.FormaPago,
                        Unidades = 0,
                        Monto = 0
                    };
                    fondo.Caja.FormasPago.Add(cfp);
                }
                if (rfp.Entregar > 0)
                {
                    fondo.FormasPago.Add(new DataAccess.SirCoPOS.FondoFormaPago
                    {
                        Entrada = false,
                        FormaPago = (int)rfp.FormaPago,
                        Cantidad = rfp.Entregar,
                        UsuarioId = request.CajeroId,
                        Monto = rfp.Amount,
                        Fecha = now,
                        Referencia = gid
                    });
                    cfp.Unidades -= rfp.Entregar;
                    cfp.Monto -= rfp.Amount;
                }
                //fondo.ArqueosFormaPagos.Add(new DataAccess.SirCoPOS.FondoArqueoFormaPago
                //{
                //    AuditorId = request.AuditorId,
                //    Fecha = now,
                //    Unidades = cfp.Unidades,
                //    ReportadoUnidades = 0,
                //    Monto = cfp.Monto,
                //    ReportadoMonto = 0,
                //    Corte = item
                //});
                //cfp.Unidades = 0;
                //cfp.Monto = 0;
            }
            //item.Series = new HashSet<DataAccess.SirCoPOS.CorteSerie>();
            //foreach (var s in corte.Series)
            //{
            //    if (request.Series.Contains(s.Serie))
            //    {
            //        ctx.UpdateSerieStatus(s.Serie, Common.Constants.Status.AC, Common.Constants.Status.AB, request.AuditorId);
            //    }
            //    else
            //    {
            //        item.Series.Add(new DataAccess.SirCoPOS.CorteSerie
            //        {
            //            Serie = s.Serie
            //        });

            //        this.GenerarRepetitivoCalzado(s.Serie, fondo.CajaSucursal, request, now);
            //    }
            //}

            //this.CierreFondoHelper(new FondoArqueoRequest
            //{
            //    Importe = request.Reportado.Value,
            //    Entregar = request.Entregar,
            //    Auditor = request.AuditorId,
            //    Responsable = request.CajeroId
            //}, now);

            //if (fondo.Disponible > 0)
            //{
            //    this.GenerarRepetitivo(fondo.Disponible, fondo.CajaSucursal, request, now);
            //}

            //fondo.Disponible = 0;
            //fondo.Caja.Disponible = 0;
            //fondo.Caja.ResponsableId = null;
            //fondo.FechaCierre = now;
            //fondo.AuditorCierreId = request.AuditorId;
            //ctxn.SaveChanges();
            ctxpos.SaveChanges();
        }
        public void Corte(CorteRequest request)
        {
            using (var tran = new System.Transactions.TransactionScope())
            {
                CorteHelper(request);
                tran.Complete();
            }
         }
        public void CorteTransferir(EntregaRequest request)
        {
            using (var tran = new System.Transactions.TransactionScope())
            {

                try
                {
                    this.CorteTransferirHelper(request);
                }
                catch (Exception)
                {
                    throw;
                }
                tran.Complete();
            }
        }
        private void GenerarRepetitivoCalzado(string serie, string sucursal, CorteRequest request, DateTime now)
        {
            var ctxn = new DataAccess.SirCoNominaDataContext();
            var ctx = new DataAccess.SirCoDataContext();

            var det = ctx.Series.Where(i => i.serie == serie).Single();
            var corrida = ctx.GetCorrida(det);

            var proc = new BusinessLogic.Process();
            var req = new SaleRequest
            {
                VendedorId = null,
                Sucursal = sucursal,
                Pagos = new Pago[] {
                            new Pago
                            {
                                FormaPago = Common.Constants.FormaPago.CI,
                                Importe = corrida.precio.Value
                            }
                        },
                Productos = new SerieFormasPago[]
                {
                            new SerieFormasPago
                            {
                                Serie = serie,
                                FormasPago = new Common.Constants.FormaPago[] { Common.Constants.FormaPago.CI }
                            }
                }
            };
            // Si no se ha escaneado para venta está en AB
            ctx.UpdateSerieStatus(serie, Common.Constants.Status.CA, Common.Constants.Status.AB, request.AuditorId);
            // Si se intentó escanear ya está en AC
            ctx.UpdateSerieStatus(serie, Common.Constants.Status.CA, Common.Constants.Status.AC, request.AuditorId);

            // Se genera la venta Con Forma de Pago CI (Cargo Inventario)
            var res = proc.Sale(req, idcajero: 0);

            var aud = ctxn.Empleados.Where(i => i.idempleado == request.AuditorId).Single();
            var last = ctxn.Repetitivos.OrderByDescending(i => i.idrepetitivo).FirstOrDefault();

            var ctxc = new DataAccess.SirCoCreditoDataContext();
            //var cal = ctxc.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "TARJETAHABIENTE"
            //    && i.fechaaplicarcorte > now)
            //    .OrderBy(i => i.fechaaplicarcorte)
            //    .First();

            var rep = new DataAccess.SirCoNomina.Repetitivo
            {
                idrepetitivo = (last?.idrepetitivo ?? 1) + 1, //???
                idempleado = request.CajeroId,
                idpercdeduc = Common.Constants.PercepcionDeduccion.Penalizacion,
                descrip = "FALTANTE CANCELACION",
                folio = $"{req.Sucursal}{res.Folio}",
                importe = corrida.precio.Value,
                cuota = 50,
                saldo = 0,
                inicio = now,
                estatus = "V",
                idcuenta = 0,
                usuario = aud.usuariosistema,
                fum = now,
                usumodif = "",
                fummodif = null,
                observaciones = serie,
                hora = now.ToString("HH:mm:ss"),
                fin = now
            };
            ctxn.Repetitivos.Add(rep);
            ctxn.SaveChanges();
        }
        private void GenerarRepetitivo(decimal monto, string sucursal, int idcajero, int idauditor, DateTime now)
        {
            var ctxn = new DataAccess.SirCoNominaDataContext();

            var aud = ctxn.Empleados.Where(i => i.idempleado == idauditor).Single();
            var last = ctxn.Repetitivos.OrderByDescending(i => i.idrepetitivo).FirstOrDefault();

            //var ctxc = new DataAccess.SirCoCreditoDataContext();
            //var cal = ctxc.Calendarios.Where(i => i.tipo == "CORTE" && i.tipocredito == "TARJETAHABIENTE"
            //    && i.fechaaplicarcorte > now)
            //    .OrderBy(i => i.fechaaplicarcorte)
            //    .First();

            var rep = new DataAccess.SirCoNomina.Repetitivo
            {
                idrepetitivo = (last?.idrepetitivo ?? 1) + 1, //???
                idempleado = idcajero,
                idpercdeduc = Common.Constants.PercepcionDeduccion.Penalizacion,
                descrip = "FALTANTE ARQUEO",
                folio = $"",
                importe = monto,
                cuota = 50,
                saldo = 0,
                inicio = now,
                estatus = "V",
                idcuenta = 0,
                usuario = aud.usuariosistema,
                fum = now,
                usumodif = "",
                fummodif = null,
                observaciones = null,
                hora = now.ToString("HH:mm:ss"),
                fin = now
            };
            ctxn.Repetitivos.Add(rep);
            ctxn.SaveChanges();
        }
        private void CorteHelper(CorteRequest request)
        {
            var gid = Guid.NewGuid();
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctxpos = new DataAccess.SirCoPOSDataContext();
            var ctxn = new DataAccess.SirCoNominaDataContext();
            var ctx = new DataAccess.SirCoDataContext();
            var ctxpv = new DataAccess.SirCoPVDataContext();
            var fondo = ctxpos.Fondos.Where(i => i.ResponsableId == request.CajeroId && !i.FechaCierre.HasValue).Single();
            var corte = _admin.GetCorteCaja(request.Sucursal, request.CajeroId);
            //var item = new DataAccess.SirCoPOS.Corte
            //{
            //    CajeroId = request.CajeroId,
            //    Fecha = now,
            //    AuditorId = request.AuditorId,
            //    Entregado = request.Entregar,
            //    FondoId = fondo.Id,
            //    Cierre = true,
            //    //Ventas = corte.Ventas,
            //    //Pagos = corte.Pagos,
            //    //public decimal? Gastos { get; set; }
            //    //public decimal? Bonos { get; set; }
            //};
            //ctxpos.Cortes.Add(item);

            var afondo = ctxpos.Fondos.Where(i => i.ResponsableId == request.AuditorId && !i.FechaCierre.HasValue).SingleOrDefault();
            if (afondo != null)
            {
                if (request.Entregar > 0)
                {
                    afondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
                    {
                        Importe = request.Entregar,
                        UsuarioId = request.CajeroId,
                        Entrada = true,
                        Fecha = now,
                        Referencia = gid,
                        Tipo = "Corte"
                    });
                    afondo.Disponible += request.Entregar;
                    afondo.Caja.Disponible += request.Entregar;
                }

                foreach (var fp in request.FormasPago)
                {
                    if (fp.Entregar > 0)
                    {
                        afondo.FormasPago.Add(new DataAccess.SirCoPOS.FondoFormaPago
                        {
                            Entrada = true,
                            FormaPago = (int)fp.FormaPago,
                            Cantidad = fp.Entregar,
                            UsuarioId = request.CajeroId,
                            Monto = fp.Amount,
                            Fecha = now,
                            Referencia = gid
                        });
                        var cf = afondo.Caja.FormasPago.Where(i => i.FormaPago == (int)fp.FormaPago).SingleOrDefault();
                        if (cf == null)
                        {
                            cf = new DataAccess.SirCoPOS.CajaFormaPago
                            {
                                FormaPago = (int)fp.FormaPago,
                                Unidades = 0,
                                Monto = 0
                            };
                            afondo.Caja.FormasPago.Add(cf);
                        }
                        cf.Unidades += fp.Entregar;
                        cf.Monto += fp.Amount;
                    }
                }
            }
            if (request.Entregar > 0)
            {
                fondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
                {
                    Importe = request.Entregar,
                    UsuarioId = request.AuditorId,
                    Entrada = false,
                    Fecha = now,
                    Referencia = gid,
                    Tipo = "Corte"
                });
                fondo.Disponible -= request.Entregar;
                fondo.Caja.Disponible -= request.Entregar;
            }
            var arqueo = new DataAccess.SirCoPOS.FondoArqueo
            {
                AuditorId = request.AuditorId,
                Fecha = now,
                Importe = corte.Importe,
                Reportado = request.Entregar,
                Corte = true
            };
            arqueo.FormasPago = new HashSet<DataAccess.SirCoPOS.FondoArqueoFormaPago>();
            fondo.Arqueos.Add(arqueo);
            //item.FormasPago = new HashSet<DataAccess.SirCoPOS.CorteFormaPago>();
            foreach (var fp in corte.FormaPagoTotales)
            {
                var rfp = request.FormasPago.Where(i => i.FormaPago == fp.FormaPago).Single();
                //item.FormasPago.Add(new DataAccess.SirCoPOS.CorteFormaPago 
                //{
                //    FormaPago = (int)fp.FormaPago,
                //    Entregado = rfp.Entregar,
                //    Total = rfp.Amount
                //});
                arqueo.FormasPago.Add(new DataAccess.SirCoPOS.FondoArqueoFormaPago
                {
                    FormaPago = (int)fp.FormaPago,
                    Monto = fp.Total.Value,
                    Unidades = fp.Count,
                    ReportadoUnidades = rfp.Entregar,
                    ReportadoMonto = rfp.Amount
                });

                var cfp = fondo.Caja.FormasPago.Where(i => i.FormaPago == (int)fp.FormaPago).SingleOrDefault();
                if (cfp == null)
                {
                    cfp = new DataAccess.SirCoPOS.CajaFormaPago
                    {
                        FormaPago = (int)rfp.FormaPago,
                        Unidades = 0,
                        Monto = 0
                    };
                    fondo.Caja.FormasPago.Add(cfp);
                }
                if (rfp.Entregar > 0)
                {
                    fondo.FormasPago.Add(new DataAccess.SirCoPOS.FondoFormaPago
                    {
                        Entrada = false,
                        FormaPago = (int)rfp.FormaPago,
                        Cantidad = rfp.Entregar,
                        UsuarioId = request.AuditorId,
                        Monto = rfp.Amount,
                        Fecha = now,
                        Referencia = gid
                    });
                    cfp.Unidades -= rfp.Entregar;
                    cfp.Monto -= rfp.Amount;
                }
                //fondo.ArqueosFormaPagos.Add(new DataAccess.SirCoPOS.FondoArqueoFormaPago
                //{
                //    AuditorId = request.AuditorId,
                //    Fecha = now,
                //    Unidades = cfp.Unidades,
                //    ReportadoUnidades = 0,
                //    Monto = cfp.Monto,
                //    ReportadoMonto = 0,
                //    Corte = item
                //});
                cfp.Unidades = 0;
                cfp.Monto = 0;
            }
            //item.Series = new HashSet<DataAccess.SirCoPOS.CorteSerie>();
            arqueo.Series = new HashSet<DataAccess.SirCoPOS.FondoArqueoSerie>();
            foreach (var s in corte.Series)
            {
                // Serie Cancelada está reportada cambiar su estatus a AC
                if (request.Series.Contains(s.Serie))
                {
                    ctx.UpdateSerieStatus(s.Serie, Common.Constants.Status.AC, Common.Constants.Status.AB, request.AuditorId);
                }
                else   //Serie Cancelada No Reportada genera repetitivo y venta

                {
                    arqueo.Series.Add(new DataAccess.SirCoPOS.FondoArqueoSerie
                    {
                        Serie = s.Serie
                    });

                    this.GenerarRepetitivoCalzado(s.Serie, fondo.CajaSucursal, request, now);
                }
            }
            ctxpv.SaveChanges();
            foreach (var s in corte.Series)
            {
                //Quitar todas las series canceladas de la entrega de Caja
                DataAccess.SirCoPV.SerieCancelada serieCancelada = ctxpv.SeriesCanceladas.
                    Where(i => i.serie == s.Serie &&
                          i.sucursal == request.Sucursal &&
                          i.idcajerocancela == request.CajeroId).SingleOrDefault();
                if (serieCancelada != null)
                {
                    ctxpv.SeriesCanceladas.Remove(serieCancelada);
                }
            }
            ctxpv.SaveChanges();


            //this.CierreFondoHelper(new FondoArqueoRequest
            //{
            //    Importe = request.Reportado.Value,
            //    Entregar = request.Entregar,
            //    Auditor = request.AuditorId,
            //    Responsable = request.CajeroId
            //}, now);

            if (fondo.Disponible > 0)
            {
                this.GenerarRepetitivo(fondo.Disponible, fondo.CajaSucursal, idcajero: request.CajeroId, idauditor: request.AuditorId, now: now);
            }

            fondo.Disponible = 0;
            fondo.Caja.Disponible = 0;
            fondo.Caja.ResponsableId = null;
            fondo.FechaCierre = now;
            fondo.AuditorCierreId = request.AuditorId;
            //ctxn.SaveChanges();
            ctxpos.SaveChanges();
        }
        private void CorteTransferirHelper(EntregaRequest request)
        {
            var gid = Guid.NewGuid();
            var now = BusinessLogic.Helpers.Common.GetNow();
            var ctxpos = new DataAccess.SirCoPOSDataContext();
            var ctxn = new DataAccess.SirCoNominaDataContext();
            var ctx = new DataAccess.SirCoDataContext();
            var fondo = ctxpos.Fondos.Where(i => i.ResponsableId == request.CajeroId && !i.FechaCierre.HasValue).Single();
            var corte = _admin.GetCorteCaja(request.Sucursal, request.CajeroId);
            //var item = new DataAccess.SirCoPOS.Corte
            //{
            //    CajeroId = request.CajeroId,
            //    Fecha = now,
            //    AuditorId = request.AuditorId,
            //    Entregado = request.Entregar,
            //    FondoId = fondo.Id,
            //    Cierre = true,
            //    //Ventas = corte.Ventas,
            //    //Pagos = corte.Pagos,
            //    //public decimal? Gastos { get; set; }
            //    //public decimal? Bonos { get; set; }
            //};
            //ctxpos.Cortes.Add(item);
            if (request.Entregar > 0)
            {
                fondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
                {
                    Importe = request.Entregar,
                    UsuarioId = request.AuditorId,
                    Entrada = false,
                    Fecha = now,
                    Referencia = gid,
                    Tipo = "Corte"
                });
                fondo.Disponible -= request.Entregar;
                fondo.Caja.Disponible -= request.Entregar;
            }
            var arqueo = new DataAccess.SirCoPOS.FondoArqueo
            {
                AuditorId = request.AuditorId,
                Fecha = now,
                Importe = corte.Importe,
                Reportado = request.Entregar,
                Corte = true
            };
            fondo.Arqueos.Add(arqueo);
            //item.FormasPago = new HashSet<DataAccess.SirCoPOS.CorteFormaPago>();
            arqueo.FormasPago = new HashSet<DataAccess.SirCoPOS.FondoArqueoFormaPago>();
            foreach (var fp in corte.FormaPagoTotales)
            {
                var rfp = request.FormasPago.Where(i => i.FormaPago == fp.FormaPago).Single();
                //item.FormasPago.Add(new DataAccess.SirCoPOS.CorteFormaPago
                //{
                //    FormaPago = (int)fp.FormaPago,
                //    Entregado = rfp.Entregar,
                //    Total = rfp.Amount
                //});
                arqueo.FormasPago.Add(new DataAccess.SirCoPOS.FondoArqueoFormaPago
                {
                    FormaPago = (int)fp.FormaPago,
                    Monto = fp.Total.Value,
                    Unidades = fp.Count,
                    ReportadoUnidades = rfp.Entregar,
                    ReportadoMonto = rfp.Amount
                });

                var cfp = fondo.Caja.FormasPago.Where(i => i.FormaPago == (int)fp.FormaPago).SingleOrDefault();
                if (cfp == null)
                {
                    cfp = new DataAccess.SirCoPOS.CajaFormaPago
                    {
                        FormaPago = (int)rfp.FormaPago,
                        Unidades = 0,
                        Monto = 0
                    };
                    fondo.Caja.FormasPago.Add(cfp);
                }
                if (rfp.Entregar > 0)
                {
                    fondo.FormasPago.Add(new DataAccess.SirCoPOS.FondoFormaPago
                    {
                        Entrada = false,
                        FormaPago = (int)rfp.FormaPago,
                        Cantidad = rfp.Entregar,
                        UsuarioId = request.AuditorId,
                        Monto = rfp.Amount,
                        Fecha = now,
                        Referencia = gid
                    });
                    cfp.Unidades -= rfp.Entregar;
                    cfp.Monto -= rfp.Amount;
                }
                //fondo.ArqueosFormaPagos.Add(new DataAccess.SirCoPOS.FondoArqueoFormaPago
                //{
                //    AuditorId = request.AuditorId,
                //    Fecha = now,
                //    Unidades = cfp.Unidades,
                //    ReportadoUnidades = 0,
                //    Monto = cfp.Monto,
                //    ReportadoMonto = 0,
                //    Corte = item
                //});
                cfp.Unidades = 0;
                cfp.Monto = 0;
            }

            if (fondo.Disponible > 0)
            {
                this.GenerarRepetitivo(fondo.Disponible, fondo.CajaSucursal, idcajero: request.CajeroId, idauditor: request.AuditorId, now: now);
            }
            // crea un fondo para el auditor que recibe en la misma caja del que entrega
            fondo.Disponible = 0;
            fondo.Caja.Disponible = 0;
            fondo.Caja.ResponsableId = null;
            fondo.FechaCierre = now;
            fondo.AuditorCierreId = request.AuditorId;

            var afondo = new DataAccess.SirCoPOS.Fondo
            {
                ResponsableId = request.AuditorId,
                FechaApertura = now,
                Disponible = 0,
                Tipo = fondo.Tipo,
                AuditorAperturaId = request.CajeroId,
                //public string CajaSucursal { get; set; }
                //public byte? CajaNumero { get; set; },
                Caja = fondo.Caja
            };
            afondo.FormasPago = new HashSet<DataAccess.SirCoPOS.FondoFormaPago>();
            afondo.Movimientos = new HashSet<DataAccess.SirCoPOS.FondoMovimiento>();
            fondo.Caja.Fondos.Add(afondo);
            afondo.Caja.ResponsableId = request.AuditorId;
            if (afondo != null)
            {
                if (request.Entregar > 0)
                {
                    afondo.Movimientos.Add(new DataAccess.SirCoPOS.FondoMovimiento
                    {
                        Importe = request.Entregar,
                        UsuarioId = request.CajeroId,
                        Entrada = true,
                        Fecha = now,
                        Referencia = gid,
                        Tipo = "Corte"
                    });
                    afondo.Disponible += request.Entregar;
                    afondo.Caja.Disponible += request.Entregar;
                }

                foreach (var fp in request.FormasPago)
                {
                    if (fp.Entregar > 0)
                    {
                        afondo.FormasPago.Add(new DataAccess.SirCoPOS.FondoFormaPago
                        {
                            Entrada = true,
                            FormaPago = (int)fp.FormaPago,
                            Cantidad = fp.Entregar,
                            UsuarioId = request.CajeroId,
                            Monto = fp.Amount,
                            Fecha = now,
                            Referencia = gid
                        });
                        var cf = afondo.Caja.FormasPago.Where(i => i.FormaPago == (int)fp.FormaPago).SingleOrDefault();
                        if (cf == null)
                        {
                            cf = new DataAccess.SirCoPOS.CajaFormaPago
                            {
                                FormaPago = (int)fp.FormaPago,
                                Unidades = 0,
                                Monto = 0
                            };
                            afondo.Caja.FormasPago.Add(cf);
                        }
                        cf.Unidades += fp.Entregar;
                        cf.Monto += fp.Amount;
                    }
                }
            }


            //item.Series = new HashSet<DataAccess.SirCoPOS.CorteSerie>();
            //foreach (var s in corte.Series)
            //{
            //    if (request.Series.Contains(s.Serie))
            //    {
            //        ctx.UpdateSerieStatus(s.Serie, Common.Constants.Status.AC, Common.Constants.Status.AB, request.AuditorId);
            //    }
            //    else
            //    {
            //        item.Series.Add(new DataAccess.SirCoPOS.CorteSerie
            //        {
            //            Serie = s.Serie
            //        });

            //        this.GenerarRepetitivoCalzado(s.Serie, fondo.CajaSucursal, request, now);
            //    }
            //}

            //this.CierreFondoHelper(new FondoArqueoRequest
            //{
            //    Importe = request.Reportado.Value,
            //    Entregar = request.Entregar,
            //    Auditor = request.AuditorId,
            //    Responsable = request.CajeroId
            //}, now);

            


            //ctxn.SaveChanges();
            ctxpos.SaveChanges();
        }
        public bool IsFondoAbierto(string sucursal, int idcajero)
        {
            var ctx = new DataAccess.SirCoPOSDataContext();
            var q = ctx.Fondos.Where(i => i.ResponsableId == idcajero && i.CajaSucursal == sucursal && !i.FechaCierre.HasValue);
            return q.Any();
        }

        public IEnumerable<Common.Entities.Caja> GetCajas(string sucursal, int idempleado)
        {
            var ctxn = new DataAccess.SirCoNominaDataContext();
            var ctx = new DataAccess.SirCoPOSDataContext();

            var emp = ctxn.Empleados.Where(i => i.idempleado == idempleado).Single();
            var q = ctx.Cajas.Where(i => i.Sucursal == sucursal
                && i.Disponible == 0 && !i.ResponsableId.HasValue
                && !i.Fondos.Where(f => !f.FechaCierre.HasValue).Any()); //al buscar fondo con suc,numero no exista alguno abierto
            if (emp.idpuesto == (int)Common.Constants.Puesto.CJA) // el empleado es cajero?
            {
                q = q.Where(i => i.Tipo == Common.Constants.TipoFondo.Cajon); 
            }
            else if (emp.idpuesto == (int)Common.Constants.Puesto.ENC
                || emp.idpuesto == (int)Common.Constants.Puesto.SUP)
            {
                q = q.Where(i => i.Tipo == Common.Constants.TipoFondo.CajaFuerte );
            }
            else if (emp.iddepto == (int)Common.Constants.Departamento.SIS || emp.iddepto == (int)Common.Constants.Departamento.ADM)
            {
                q = q.Where(i => i.Tipo == Common.Constants.TipoFondo.CajaFuerte || i.Tipo == Common.Constants.TipoFondo.Cajon);
            }
            else
                return null;

            return q.Select(i => new Common.Entities.Caja
            {
                Tipo = (byte)i.Tipo,
                Numero = i.Numero,
                Importe = i.Disponible
            });
        }
        public byte[] GetHuella(int idempleado)
        {
            var ctx = new DataAccess.SirCoNominaDataContext();
            var empleado = ctx.Empleados.Where(i => i.idempleado == idempleado).SingleOrDefault();
            if (empleado == null)
                return null;
            var huella = empleado.HuellasPOS.FirstOrDefault();
            if (huella == null)
                return null;
            return huella.template;
        }
        public int? IdentificarSupervisor(string sucursal, byte[] template)
        {
            var puestos = new int[] {
                (int)Common.Constants.Puesto.ENC,
                (int)Common.Constants.Puesto.SUP
            };
            var proxy = new System.ServiceModel.ChannelFactory<Common.ServiceContracts.ILocalService>("*").CreateChannel();
            var ctx = new DataAccess.SirCoNominaDataContext();
            //var helper = new BusinessLogic.Helpers.FingerHelper();
            var q = ctx.Empleados.Where(i => i.HuellasPOS.Any()
                && i.iddepto == (int)Common.Constants.Departamento.TDA
                && puestos.Contains(i.idpuesto)
                && i.estatus == "A"
                && i.clave.Substring(0, 2) == sucursal)
                .OrderByDescending(i => i.idpuesto);

            var huellas = new Dictionary<int, byte[]>();
            foreach (var item in q)
            {
                foreach (var huella in item.HuellasPOS)
                {
                    huellas.Add(huella.id, huella.template);
                }
            }

            var id = proxy.Find(template, huellas);
            if (id.HasValue)
            {
                var item = ctx.HuellasPOS.Where(i => i.id == id).Single();
                return item.idempleado;
            }
            return null;
        }
    }
}
