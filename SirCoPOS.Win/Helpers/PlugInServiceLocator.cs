using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SirCoPOS.Win.Helpers
{
    public class PlugInServiceLocator
    {
        private AggregateCatalog _catalog;
        private CompositionContainer _container;
        public PlugInServiceLocator()
        {
            _catalog = new AggregateCatalog();
            _container = new CompositionContainer(_catalog);

            //var rb = new RegistrationBuilder();
            //rb.ForTypesDerivedFrom<UserControl>()
            //    .

        }
        public void Export(string assembly)
        {
            var path = String.Format(@"{0}\{1}.dll", AppDomain.CurrentDomain.RelativeSearchPath ?? ".", assembly);
            var ac = new AssemblyCatalog(path);
            _catalog.Catalogs.Add(ac);
        }
        public void Export(Assembly assembly)
        {
            var ac = new AssemblyCatalog(assembly);
            _catalog.Catalogs.Add(ac);
        }
        public void Export<T>()
        {
            var typeCatalog = new TypeCatalog(typeof(T));
            _catalog.Catalogs.Add(typeCatalog);
        }
        public UserControl GetView(Utilities.Constants.TabType view)
        {
            //var q = _container.GetExports<UserControl, IDictionary<string, object>>()
            //    .Where(i => (Utilities.Constants.TabType)i.Metadata["View"] == view);
            var q = _container.GetExports<UserControl, Utilities.Extensions.ITab>()
                .Where(i => i.Metadata.Tab == view);
            var res = q.SingleOrDefault();
            if (res != null)
                return res.Value;
            return null;
        }
        public UserControl GetPagoView(Common.Constants.FormaPago view)
        {
            var q = _container.GetExports<UserControl, Utilities.Extensions.IFormaPago>("pago")
                .Where(i => i.Metadata.FormaPago == view);
            var res = q.SingleOrDefault();
            if (res != null)
                return res.Value;
            return null;
        }
        public UserControl GetModalView(Utilities.Constants.Modals modal)
        {
            var q = _container.GetExports<UserControl, Utilities.Extensions.IModal>("modal")
                .Where(i => i.Metadata.Modal == modal);
            var res = q.SingleOrDefault();
            if (res != null)
                return res.Value;
            return null;
        }
    }
}
