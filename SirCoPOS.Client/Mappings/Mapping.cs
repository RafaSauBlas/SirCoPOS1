using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Mappings
{
    class Mapping
    {
        [Common.Mapping]
        public void Register(IMapperConfigurationExpression map)
        {
            map.CreateMap<Common.Entities.Producto, Models.Producto>()
                    .ForMember(d => d.PrecioOriginal, s => s.MapFrom(o => o.Precio))
                    ;
            map.CreateMap<Common.Entities.Cliente, Models.NuevoCliente>();
            map.CreateMap<Models.NuevoCliente, Common.Entities.Cliente>();
            map.CreateMap<Models.NuevoCliente, Utilities.Entities.NuevoCliente>();
            map.CreateMap<Utilities.Entities.NuevoCliente, Models.NuevoCliente>();
        }
    }
}
