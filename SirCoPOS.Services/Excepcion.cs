using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Services
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

    public class CajaNoDisponibleExcepcion : Common.Helpers.BaseExcepcion
    {
        public CajaNoDisponibleExcepcion()
            : base("No se Localizó Caja Disponible")
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
    public class SameId : Common.Helpers.BaseExcepcion
    {

        public SameId()
            : base("No puedes utilizar tu propio Id como auditor")
        {
        }
    }
}
