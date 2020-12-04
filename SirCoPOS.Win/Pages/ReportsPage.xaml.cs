using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SirCoPOS.Win.Helpers;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Imaging;

namespace SirCoPOS.Win.Pages
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = new SirCoPOS.Reports.Entities.ReciboPago
            {
                Sucursal = "99",
                Folio = "536279",
                Fecha = DateTime.Parse("2019-03-20T09:58:12"),
                ClienteId = "000498",
                ClienteNombre = "MARTINA AVILA ROBLES",
                Cantidad = 8446.74m,
                Concepto = "Abono al Corte 0349",
                Descuento = 1618.26m,
                Vencido = 0,
                Recibido = "ROEL1963",
                Disponible = 204659.71m,
                Saldo = 64032.29m,
                CajeroId = 10,
                CajeroNombre = "nombre de cajero",
                VendedorId = 99,
                VendedorNombre = "nombre de vendedor"
            };
            item.CantidadLetra = item.Cantidad.ToLetras();
            var list = new List<SirCoPOS.Reports.Entities.ReciboPago>() { item };
            var dic = new Dictionary<string, IEnumerable<object>>() { { "DataSet1", list } };
            var win = new Windows.ViewerWindow(
                fullname: "SirCoPOS.Reports.ReciboAbono.rdlc", 
                library: "SirCoPOS.Reports", 
                datasources: dic);
            win.Show();
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            var item = new SirCoPOS.Reports.Entities.ReciboCompra
            {
                SucursalId = "01",
                SucursalNombre = "JUAREZ",
                RFC = "CTO-911211JL9",
                Direccion = "AV. JUAREZ # 1015 PTE.",
                Colonia = "CENTRO",
                Fecha = DateTime.Parse("2019-06-28T11:41:53"),
                VendedorId = "99",
                VendedorNombre = "ZAPTORRE",
                CajeroId = "99",
                CajeroNombre = "SISTEMAS",
                Folio = "433690",
                Efectivo = 3975,
                Descuento = 500,
                Cambio = DateTime.Parse("2019-07-13")
            };
            //item.CantidadLetra = item.Cantidad.ToLetras();
            var list = new List<SirCoPOS.Reports.Entities.ReciboCompra>() { item };
            var plist = new List<SirCoPOS.Reports.Entities.Producto>();
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002711582", Precio = 849, Importe = 849, Marca = "PPP", Descripcion = "9611 HORMA NORMAL NE" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002660601", Precio = 739, Importe = 739, Marca = "PPP", Descripcion = "9745-2 P. NEGRO" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002755737", Precio = 589, Importe = 589, Marca = "PPP", Descripcion = "9611 HORMA NORMAL NE" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000003641641", Precio = 1149, Importe = 899, Marca = "NIK", Descripcion = "7494747-411 NIKE COUR" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000003641649", Precio = 1149, Importe = 899, Marca = "NIK", Descripcion = "7494747-411 NIKE COUR" });

            var pdlist = new List<SirCoPOS.Reports.Entities.Pago>();
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "EF", Importe = 3999m });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "TC", Importe = 3999m, Referencia = "123", Folio = "999" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "TD", Importe = 3999m, Referencia = "123", Folio = "999" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "DV", Importe = 3999m, Referencia = "08123456" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "VA", Importe = 3999m, Referencia = "1000" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "CP", Importe = 3999m });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "CV", Importe = 3999m, Referencia = "45621" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "MD", Importe = 3999m });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "VD", Importe = 3999m, Referencia = "45621" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "CD", Importe = 3999m });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "VE", Importe = 3999m, Referencia = "23136" });

            var pplist = new List<SirCoPOS.Reports.Entities.PlanPago>();
            pplist.Add(new SirCoPOS.Reports.Entities.PlanPago { Pago = 1, Importe = 100m, Date = new DateTime(2020, 10, 15) });
            pplist.Add(new SirCoPOS.Reports.Entities.PlanPago { Pago = 2, Importe = 200m, Date = new DateTime(2020, 10, 20) });
            pplist.Add(new SirCoPOS.Reports.Entities.PlanPago { Pago = 3, Importe = 300m, Date = new DateTime(2020, 10, 25) });
            pplist.Add(new SirCoPOS.Reports.Entities.PlanPago { Pago = 4, Importe = 400m, Date = new DateTime(2020, 11, 05) });
            pplist.Add(new SirCoPOS.Reports.Entities.PlanPago { Pago = 5, Importe = 500m, Date = new DateTime(2020, 11, 10) });

            var dic = new Dictionary<string, IEnumerable<object>>() {
                { "reciboDataSet", list },
                { "productosDataSet", plist },
                { "pagosDataSet", pdlist },
                { "planPagosDataSet", pplist }
            };
            var win = new Windows.ViewerWindow(
                fullname: "SirCoPOS.Reports.ReciboVenta.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dic);
            win.Show();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var item = new SirCoPOS.Reports.Entities.ReciboDevolucion
            {
                SucursalId = "01",
                SucursalNombre = "JUAREZ",
                Fecha = DateTime.Parse("2019-06-28T12:12:38"),
                VendedorId = "99",
                VendedorNombre = "ZAPTORRE",
                CajeroId = "99",
                CajeroNombre = "SISTEMAS",
                Folio = "433690",
                FolioVenta = "029332"                
            };
            var list = new List<SirCoPOS.Reports.Entities.ReciboDevolucion>() { item };
            var plist = new List<SirCoPOS.Reports.Entities.Producto>();
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002711582", Precio = 849, Importe = 849, Marca = "PPP", Descripcion = "9611 HORMA NORMAL NE" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002660601", Precio = 739, Importe = 739, Marca = "PPP", Descripcion = "9745-2 P. NEGRO" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002755737", Precio = 589, Importe = 589, Marca = "PPP", Descripcion = "9611 HORMA NORMAL NE" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000003641641", Precio = 1149, Importe = 899, Marca = "NIK", Descripcion = "7494747-411 NIKE COUR" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000003641649", Precio = 1149, Importe = 899, Marca = "NIK", Descripcion = "7494747-411 NIKE COUR" });
            var dic = new Dictionary<string, IEnumerable<object>>() {
                { "devolucionDataSet", list },
                { "productosDataSet", plist }
            };
            var win = new Windows.ViewerWindow(
                fullname: "SirCoPOS.Reports.ReciboDevolucion.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dic);
            win.Show();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            var item = new SirCoPOS.Reports.Entities.ReciboPago
            {
                Sucursal = "99",
                Folio = "536279",
                Fecha = DateTime.Parse("2019-03-20T09:58:12"),
                ClienteId = "000498",
                ClienteNombre = "MARTINA AVILA ROBLES",
                Cantidad = 8446.74m,
                Concepto = "Abono al Corte 0349",
                Descuento = 1618.26m,
                Vencido = 0,
                Recibido = "ROEL1963",
                Disponible = 204659.71m,
                Saldo = 64032.29m,
                CajeroId = 10,
                CajeroNombre = "nombre de cajero",
                VendedorId = 99,
                VendedorNombre = "nombre de vendedor"
            };
            item.CantidadLetra = item.Cantidad.ToLetras();
            var list = new List<SirCoPOS.Reports.Entities.ReciboPago>() { item };
            var dic = new Dictionary<string, IEnumerable<object>>() { { "DataSet1", list } };
            var pd = new Helpers.PrintFile(
                fullname: "SirCoPOS.Reports.ReciboAbono.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dic);
            pd.Print();
        }        

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            var item = new SirCoPOS.Reports.Entities.ReciboCompra
            {
                SucursalId = "01",
                SucursalNombre = "JUAREZ",
                RFC = "CTO-911211JL9",
                Direccion = "AV. JUAREZ # 1015 PTE.",
                Colonia = "CENTRO",
                Fecha = DateTime.Parse("2019-06-28T11:41:53"),
                VendedorId = "99",
                VendedorNombre = "ZAPTORRE",
                CajeroId = "99",
                CajeroNombre = "SISTEMAS",
                Folio = "433690",
                Efectivo = 3975,
                Descuento = 500,
                Cambio = DateTime.Parse("2019-07-13")
            };
            //item.CantidadLetra = item.Cantidad.ToLetras();
            var list = new List<SirCoPOS.Reports.Entities.ReciboCompra>() { item };
            var plist = new List<SirCoPOS.Reports.Entities.Producto>();
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002711582", Precio = 849, Importe = 849, Marca = "PPP", Descripcion = "9611 HORMA NORMAL NE" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002660601", Precio = 739, Importe = 739, Marca = "PPP", Descripcion = "9745-2 P. NEGRO" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002755737", Precio = 589, Importe = 589, Marca = "PPP", Descripcion = "9611 HORMA NORMAL NE" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000003641641", Precio = 1149, Importe = 899, Marca = "NIK", Descripcion = "7494747-411 NIKE COUR" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000003641649", Precio = 1149, Importe = 899, Marca = "NIK", Descripcion = "7494747-411 NIKE COUR" });

            var pdlist = new List<SirCoPOS.Reports.Entities.Pago>();
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "EF", Importe = 3999m  });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "TC", Importe = 3999m, Referencia = "123", Folio = "999" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "TD", Importe = 3999m, Referencia = "123", Folio = "999" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "DV", Importe = 3999m, Referencia = "08123456" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "VA", Importe = 3999m, Referencia = "1000" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "CP", Importe = 3999m });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "CV", Importe = 3999m, Referencia = "45621" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "MD", Importe = 3999m });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "VD", Importe = 3999m, Referencia = "45621" });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "CD", Importe = 3999m });
            pdlist.Add(new SirCoPOS.Reports.Entities.Pago { FormaPago = "VE", Importe = 3999m, Referencia = "23136" });

            var dic = new Dictionary<string, IEnumerable<object>>() {
                { "reciboDataSet", list },
                { "productosDataSet", plist },
                { "pagosDataSet", pdlist }
            };
            var pd = new Helpers.PrintFile(
                fullname: "SirCoPOS.Reports.ReciboVenta.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dic);
            pd.Print();
        }

        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            var item = new SirCoPOS.Reports.Entities.ReciboDevolucion
            {
                SucursalId = "01",
                SucursalNombre = "JUAREZ",
                Fecha = DateTime.Parse("2019-06-28T12:12:38"),
                VendedorId = "99",
                VendedorNombre = "ZAPTORRE",
                CajeroId = "99",
                CajeroNombre = "SISTEMAS",
                Folio = "433690",
                FolioVenta = "029332"
            };
            var list = new List<SirCoPOS.Reports.Entities.ReciboDevolucion>() { item };
            var plist = new List<SirCoPOS.Reports.Entities.Producto>();
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002711582", Precio = 849, Importe = 849, Marca = "PPP", Descripcion = "9611 HORMA NORMAL NE" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002660601", Precio = 739, Importe = 739, Marca = "PPP", Descripcion = "9745-2 P. NEGRO" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000002755737", Precio = 589, Importe = 589, Marca = "PPP", Descripcion = "9611 HORMA NORMAL NE" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000003641641", Precio = 1149, Importe = 899, Marca = "NIK", Descripcion = "7494747-411 NIKE COUR" });
            plist.Add(new SirCoPOS.Reports.Entities.Producto { Serie = "0000003641649", Precio = 1149, Importe = 899, Marca = "NIK", Descripcion = "7494747-411 NIKE COUR" });
            var dic = new Dictionary<string, IEnumerable<object>>() {
                { "devolucionDataSet", list },
                { "productosDataSet", plist }
            };
            var pd = new Helpers.PrintFile(
                fullname: "SirCoPOS.Reports.ReciboDevolucion.rdlc",
                library: "SirCoPOS.Reports",
                datasources: dic);
            pd.Print();
        }
    }
}
