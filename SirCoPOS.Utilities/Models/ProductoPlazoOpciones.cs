using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Models
{
    public class ProductoPlazoOpciones : GalaSoft.MvvmLight.ObservableObject
    {
        public ProductoPlazoOpciones(Utilities.Interfaces.IProducto item)
        {
            this.Item = item;
            this.Plazos = Enumerable.Range(1, this.Item.MaxPlazos.Value);
        }
        public IEnumerable<int> Plazos { get; private set; }
        private int? _selectedPlazo;
        public int? SelectedPlazo
        {
            get { return _selectedPlazo; }
            set
            {
                this.Set(nameof(this.SelectedPlazo), ref _selectedPlazo, value);
            }
        }
        public Utilities.Interfaces.IProducto Item { get; private set; }
    }
}
