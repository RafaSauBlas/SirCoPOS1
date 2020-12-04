using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common
{
    public class MappingAttribute : ExportAttribute
    {
        public MappingAttribute()
            : base("mapping", typeof(Action<IMapperConfigurationExpression>))
        {

        }
    }
}
