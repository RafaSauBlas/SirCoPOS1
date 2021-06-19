using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SirCoPOS.Client.Views.Caja;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Entities;
using SirCoPOS.Client;
using System.Windows.Navigation;

namespace SirCoPOS.Win.Windows
{
    /// <summary>
    /// Lógica de interacción para ClienteWindow.xaml
    /// </summary>
    public partial class ClienteWindow : Window
    {
        public string nombre,appa,apma,celular,calle,colonia,numero,cp,ciudad,estado,sexo,email;
        private Common.ServiceContracts.IDataServiceAsync _proxy;
        public ClienteWindow(List<Cliente> lista)
        {

             InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
            _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            List<Filas> filas = new List<Filas>();

            foreach (Cliente listo in lista)
            {
                Filas fila = new Filas();
                fila.Nombre = listo.Nombre;
                fila.ApPaterno = listo.ApPaterno;
                fila.ApMaterno = listo.ApMaterno;
                fila.Direccion = listo.Calle.ToString(); 
                fila.Celular = listo.Celular.ToString();
                filas.Add(fila);
                //====================================================
                fila.Calle = listo.Calle.ToString();
                fila.Colonia = listo.Colonia.ToString();
                fila.Numero = listo.Numero.ToString();
                fila.CodigoPostal = listo.CodigoPostal.ToString();
                fila.Ciudad = listo.Ciudad.ToString();
                fila.Estado = listo.Estado.ToString();
                fila.Sexo = listo.Sexo.ToString();
                fila.Email = listo.Email.ToString();
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
            public string Calle { set; get; }
            public string Colonia { set; get; }
            public string Numero { set; get; }
            public string CodigoPostal { set; get; }
            public string Ciudad { set; get; }
            public string Estado { set; get; }
            public string Sexo { set; get; }
            public string Email { set; get; }
        }
        private Guid _guid;
        public Guid GID
        {
            get => _guid;
            protected set
            {
                if (_guid == Guid.Empty)
                    _guid = value;
            }
        }

        private void dgv_ClientesInfo_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //var dato = (ClienteWindow)dgv_ClientesInfo.SelectedItem;
            this.Close();

            nombre = ((Filas)dgv_ClientesInfo.SelectedItem).Nombre;
            appa = ((Filas)dgv_ClientesInfo.SelectedItem).ApPaterno;
            apma = ((Filas)dgv_ClientesInfo.SelectedItem).ApMaterno;
            celular = ((Filas)dgv_ClientesInfo.SelectedItem).Celular;
            calle = ((Filas)dgv_ClientesInfo.SelectedItem).Calle;
            colonia = ((Filas)dgv_ClientesInfo.SelectedItem).Colonia;
            numero = ((Filas)dgv_ClientesInfo.SelectedItem).Numero;
            cp = ((Filas)dgv_ClientesInfo.SelectedItem).CodigoPostal;
            ciudad = ((Filas)dgv_ClientesInfo.SelectedItem).Ciudad;
            estado = ((Filas)dgv_ClientesInfo.SelectedItem).Estado;
            sexo = ((Filas)dgv_ClientesInfo.SelectedItem).Sexo;
            email = ((Filas)dgv_ClientesInfo.SelectedItem).Email;

            var co = _proxy.FindColonia(Convert.ToInt32(colonia));
            var ciu = _proxy.FindCiudad(Convert.ToInt32(ciudad));
            var est = _proxy.FindEstado(Convert.ToInt32(estado));

            SirCoPOS.Common.Constants.ClienteInfo.nombre = nombre;
            SirCoPOS.Common.Constants.ClienteInfo.appa = appa;
            SirCoPOS.Common.Constants.ClienteInfo.apma = apma;
            SirCoPOS.Common.Constants.ClienteInfo.celular = celular;
            SirCoPOS.Common.Constants.ClienteInfo.calle = calle;
            SirCoPOS.Common.Constants.ClienteInfo.colonia = co;
            SirCoPOS.Common.Constants.ClienteInfo.numero = numero;
            SirCoPOS.Common.Constants.ClienteInfo.cp = cp;
            SirCoPOS.Common.Constants.ClienteInfo.ciudad = ciu;
            SirCoPOS.Common.Constants.ClienteInfo.estado = est;
            SirCoPOS.Common.Constants.ClienteInfo.sexo = sexo;
            SirCoPOS.Common.Constants.ClienteInfo.email = email;

            Messenger.Default.Send(
                           new Utilities.Messages.OpenModal
                           {
                               Name = Utilities.Constants.Modals.cliente,
                               GID = GID,
                               opcion = 1
                           }
                           );

            
        }
    }
}
