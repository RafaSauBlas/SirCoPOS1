using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.BusinessLogic
{
    public class Excepcion : Common.Helpers.BaseExcepcion
    {
        //Utiliza valores default
        public Excepcion() 
        {
        }
        //Acepta un mensaje
        public Excepcion(string msg)
            : base(msg)
        {
            if (String.IsNullOrEmpty(msg))
                throw new NotSupportedException();
        }
        //Acepta un mensaje y una excepcion interna.
        public Excepcion(string message, Exception inner) 
            : base(message, inner) 
        { 
        }
    }

    public class NoDisponibleExcepcion : Common.Helpers.BaseExcepcion
    {
        public NoDisponibleExcepcion()
            : base("Importe de Compra excede Disponible de Vale")
        {
        }
    }

    public class NoExisteDistibuidorExcepcion : Common.Helpers.BaseExcepcion
    {
        public NoExisteDistibuidorExcepcion()
            : base("No se Localizó Distribuidor")
        {
        }
    }
    public class NoExisteValeExcepcion : Common.Helpers.BaseExcepcion
    {
        public NoExisteValeExcepcion()
            : base("No existe Vale")
        {
        }
    }
    public class ValeCanceladoExcepcion : Common.Helpers.BaseExcepcion
    {
        public ValeCanceladoExcepcion()
            : base("Vale Cancelado")
        {
        }
    }
    public class ClienteNOCoincideExcepcion : Common.Helpers.BaseExcepcion
    {
        public ClienteNOCoincideExcepcion()
            : base("El Cliente no es el mismo del Vale")
        {
        }
    }

    public class VigenciaVencidaExcepcion : Common.Helpers.BaseExcepcion
    {
        public VigenciaVencidaExcepcion()
            : base("Expiró la Vigencia del Vale")
        {
        }
    }
    public class ModeloPagosExcepcion : Common.Helpers.BaseExcepcion
    {
        public ModeloPagosExcepcion()
            : base("No se generó información de Pagos")
        {
        }
    }
    public class StatusSerieExcepcion : Common.Helpers.BaseExcepcion
    {
        public StatusSerieExcepcion()
            : base("El Estatus de la Serie es incorrecto")
        {
        }
    }
    public class StatusSerieNoActualizadoExcepcion : Common.Helpers.BaseExcepcion
    {
        public StatusSerieNoActualizadoExcepcion()
            : base("El Estatus de la Serie no se pudo actualizar")
        {
        }
    }
    public class DisponibleDevNoSuficienteExcepcion : Common.Helpers.BaseExcepcion
    {
        public DisponibleDevNoSuficienteExcepcion()
            : base("El Disponible de la devolución no cubre importe")
        {
        }
    }
    public class SaldoPendienteExcepcion : Common.Helpers.BaseExcepcion
    {
        public SaldoPendienteExcepcion()
            : base("Saldo Pendiente de asignar")
        {
        }
    }
    public class VentaNoExisteExcepcion : Common.Helpers.BaseExcepcion
    {
        public VentaNoExisteExcepcion()
            : base("No se localizó venta")
        {
        }
    }
    public class CancelacionNoValidaExcepcion : Common.Helpers.BaseExcepcion
    {
        public CancelacionNoValidaExcepcion()
            : base("La Venta no se hizo hoy o no está aplicada")
        {
        }
    }

    public class VentaConPagosExcepcion : Common.Helpers.BaseExcepcion
    {
        public VentaConPagosExcepcion()
            : base("La Venta se empezó a pagar")
        {
        }
    }

    public class AltaClienteExcepcion : Common.Helpers.BaseExcepcion
    {
        public AltaClienteExcepcion()
            : base("No se agregó Cliente")
        {
        }
    }

    public class TotalNoCoincideExcepcion : Common.Helpers.BaseExcepcion
    {
        public TotalNoCoincideExcepcion()
            : base("El importe no coincide con el Total")
        {
        }
    }
    public class FormaPagoNoSoportadaExcepcion : Common.Helpers.BaseExcepcion
    {
        public FormaPagoNoSoportadaExcepcion()
            : base("Forma de Pago no soportada en Sistema")
        {
        }
    }
    public class ActualizaFolioSucExcepcion : Common.Helpers.BaseExcepcion
    {
        public ActualizaFolioSucExcepcion()
            :base("Error actualizando el Folio Caja de la Sucursal")
        {
        }
    }

    public class ActualizaVentaExcepcion : Common.Helpers.BaseExcepcion
    {
        public ActualizaVentaExcepcion()
            : base("Error Actualizando la Venta")
        {
        }
        public ActualizaVentaExcepcion(string msg)
            : base( "Error Actualizando SirCoPV " + msg)
        {
        }
    }
    public class ActualizaSerieExcepcion : Common.Helpers.BaseExcepcion
    {
        public ActualizaSerieExcepcion()
            : base("Error Actualizando Serie del Producto")
        {
        }
    }
    public class ActualizaCreditoExcepcion : Common.Helpers.BaseExcepcion
    {
        public ActualizaCreditoExcepcion()
            : base("Error Actualizando Tablas de Crédito")
        {
        }
    }

    public class AgregandoPlanPagosExcepcion : Common.Helpers.BaseExcepcion
    {
        public AgregandoPlanPagosExcepcion()
            : base("Error Generando Plan de Pagos")
        {
        }
    }
    public class FondoAbiertoExcepcion : Common.Helpers.BaseExcepcion
    {
        public FondoAbiertoExcepcion()
            : base("Existe un Fondo previo sin cerrar")
        {
        }
    }
    public class NoExisteFondoAbiertoExcepcion : Common.Helpers.BaseExcepcion
    {
        public NoExisteFondoAbiertoExcepcion()
            : base("No Existe un Fondo Abierto")
        {
        }
    }
    public class AuditorOtraSucursal : Common.Helpers.BaseExcepcion
    {
        public AuditorOtraSucursal()
            : base("El auditor pertenece a otra sucursal")
        {
        }
    }
    public class SameId : Common.Helpers.BaseExcepcion
    {
        public SameId()
            : base("No puedes utilizar tu mismo Id como auditor")
        {

        }
    }
}
