using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using SirCoPOS.Common.Entities;

namespace SirCoPOS.Win.Helpers
{
    class ClienteView : Utilities.Interfaces.IClienteView
    {
        public void OpenCliente(List<Cliente> lista)
        {
            var win = new Windows.ClienteWindow(lista);

            win.ShowDialog();
        }

    }
}
