using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Helpers
{
    class Parsers
    {
        private readonly static CommonHelper _common;
        static Parsers()
        {
            _common = new CommonHelper();
        }
        public static Common.Entities.Cliente PaseCliente(
            Models.NuevoCliente ncliente, 
            Common.Entities.Cliente cliente,
            Common.Entities.Sucursal sucursal)
        {
            if (cliente != null)
            {
                return new Cliente
                {
                    Id = cliente.Id,
                    DistribuidorId = cliente.DistribuidorId
                };
            }
            else if (ncliente != null)
            {
                return new Cliente
                {
                    SucursalId = sucursal.Id,
                    Nombre = ncliente.Nombre,
                    ApPaterno = ncliente.ApPaterno,
                    ApMaterno = ncliente.ApMaterno,
                    Celular1 = ncliente.Celular1,
                    CodigoPostal = ncliente.CodigoPostal,
                    Colonia = ncliente.Colonia.Id,
                    Ciudad = ncliente.Colonia.CiudadId,
                    Estado = ncliente.Colonia.EstadoId,
                    Calle = ncliente.Calle,
                    Numero = ncliente.Numero,
                    Referencia = ncliente.Referencia,
                    Email = ncliente.Email,
                    Sexo = ncliente.Sexo,
                    Celular = ncliente.Celular,
                    Idusuario = ncliente.Idusuario
                };
            }
            return null;
        }
    }
}
