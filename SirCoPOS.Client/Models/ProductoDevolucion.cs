using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models
{
    class ProductoDevolucion : GalaSoft.MvvmLight.ObservableObject
    {
        public ProductoDevolucion()
        {
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                this.Item = new Common.Entities.ProductoDevolucion
                {
                    Id = 10,
                    Sucursal = "00",
                    Folio = "123",
                    Serie = "serie",
                    Marca = "marca",
                    Modelo = "modelo",
                    Talla = "talla",
                    Corrida = "corrida",
                    Precio = 1099.99m,
                    Pago = 799m
                };
            }
        }
        public Common.Entities.ProductoDevolucion Item { get; set; }
        [Required]
        public string Razon { get; set; }
        [Required]
        public int? RazonId { get; set; }
    }
}
