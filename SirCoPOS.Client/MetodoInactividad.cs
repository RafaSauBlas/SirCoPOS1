using System;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Windows.Shapes;
using NLog;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace SirCoPOS.Client
{
    public class MetodoInactividad
    {
        private System.Windows.Threading.DispatcherTimer _dt;
        public MetodoInactividad()
        {
            Messenger.Default.Register<string>(this, "detener", detener);
            Messenger.Default.Register<string>(this, "reiniciar", reiniciar);
            _dt = new System.Windows.Threading.DispatcherTimer();
            _dt.Tick += Dt_Tick;
            _dt.Interval = TimeSpan.FromSeconds(Common.Constants.Inactividad.Segundos);
            iniciar();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            var dt = (System.Windows.Threading.DispatcherTimer)sender;
            dt.Stop();
            Messenger.Default.Send<string>("cerrar", "Cerrar");
            Messenger.Default.Send(new Utilities.Messages.LogoutTimeout());
            //MessageBox.Show("SE HA AGOTADO EL TIEMPO XDXD");
        }


        public void iniciar()
        {
            _dt.Start();
        }

        public void detener(string msg)
        {
            if (msg == "detener")
            {
                _dt.Stop();
            }
        }

        public void reiniciar(string msg)
        {
            if (msg == "reiniciar")
            {
                _dt.Stop();
                _dt.Start();
            }
        }
    }
}
