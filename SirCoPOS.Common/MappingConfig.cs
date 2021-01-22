using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common
{
    public class MappingConfig
    {
        private AggregateCatalog _catalog;
        private CompositionContainer _container;

        public MappingConfig()
        {
            _catalog = new AggregateCatalog();
            _container = new CompositionContainer(_catalog);
        }
        public void Export(string assembly)
        {
            var path = String.Format(@"{1}.dll", AppDomain.CurrentDomain.RelativeSearchPath ?? ".", "SirCoPOS.Client");
            var ac = new AssemblyCatalog(path);
            _catalog.Catalogs.Add(ac);
        }
        public void Export<T>()
        {
            var typeCatalog = new TypeCatalog(typeof(T));
            _catalog.Catalogs.Add(typeCatalog);
        }

        public AutoMapper.IMapper Create(params Action<IMapperConfigurationExpression>[] maps)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                var mappings = _container.GetExportedValues<Action<IMapperConfigurationExpression>>("mapping");
                foreach (var map in mappings)
                {
                    map(cfg);
                }
                foreach (var map in maps)
                {
                    map(cfg);
                }
            });
            return config.CreateMapper();
        }
    }
}
