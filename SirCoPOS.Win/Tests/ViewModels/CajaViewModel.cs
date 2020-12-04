using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Tests.ViewModels
{
    class CajaViewModel
    {
        public CajaViewModel()
        {
            this.Productos = new ObservableCollection<Models.Producto>()
            {
                new Models.Producto {
                    Marca = "CHY",
                    Modelo = "3467",
                    Talla = "25.5",
                    Precio = 899,
                    Descuento = 0
                },
                new Models.Producto {
                    Marca = "ADD",
                    Modelo = "3467",
                    Talla = "25.5",
                    Precio = 899,
                    Descuento = -289
                },
                new Models.Producto {
                    Marca = "MLA",
                    Modelo = "3467",
                    Talla = "25.5",
                    Precio = 899,
                    Descuento = -289
                }
            };
            this.Selected = this.Productos[1];
            this.SubTotal = 13908;
            this.Descuento = 1890;
            this.Total = 12018;
            this.VendedorID = "0031";
            this.VendedorNombre = "Perla Erika Martos Corral";
        }

        public ObservableCollection<Models.Producto> Productos { get; set; }
        public Models.Producto Selected { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public string VendedorID { get; set; }
        public string VendedorNombre { get; set; }
    }
}
