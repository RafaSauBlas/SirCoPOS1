using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Entities;
using SirCoPOS.Client;
using System.Windows.Forms;
using System.Windows.Navigation;

namespace SirCoPOS.Win.Windows
{
    /// <summary>
    /// Lógica de interacción para ClienteWindow.xaml
    /// </summary>
    public partial class ClienteWindow : Window
    {
        public ClienteWindow(List<Cliente> lista)
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
            List<Filas> filas = new List<Filas>();

            foreach (Cliente listo in lista)
            {
                Filas fila = new Filas();
                fila.Nombre = listo.Nombre;
                fila.ApPaterno = listo.ApPaterno;
                fila.ApMaterno = listo.ApMaterno;
                fila.Direccion = listo.Calle.ToString() + ", " + listo.Colonia.ToString() + ", " + listo.Numero + ", " + listo.CodigoPostal.ToString() + ", " + listo.Colonia.ToString(); 
                fila.Celular = listo.Celular.ToString();
                filas.Add(fila);
            };

            dgv_ClientesInfo.ItemsSource = filas;
        }

        public class Filas
        {
            public string Nombre { set; get; }
            public string ApPaterno { set; get; }
            public string ApMaterno { set; get; }
            public string Celular { set; get; }
            public string Direccion { set; get; }
        }

        private void dgv_ClientesInfo_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SirCoPOS.Client.ViewModels.Caja.LoadClienteViewModel cliente = new SirCoPOS.Client.ViewModels.Caja.LoadClienteViewModel();
            SirCoPOS.Client.Views.Caja.LoadClienteSearchView clientote = new SirCoPOS.Client.Views.Caja.LoadClienteSearchView();

            this.Close();


        }
    }
}
