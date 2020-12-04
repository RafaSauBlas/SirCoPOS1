using GalaSoft.MvvmLight.Command;
using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    partial class CajaViewModel
    {
        #region computed
        public decimal? Monedero
        {
            get { 
                var mon = this.Productos.Where(i => i.Monedero.HasValue).Sum(i => i.Monedero.Value);
                if (mon > 0)
                    return mon;
                return null;
            }
        }
        public bool HasVale
        {
            get => this.Pagos.Where(i => i.FormaPago == FormaPago.VA).Any();
        }
        public bool HasPagos
        {
            get { return this.Pagos.Any(); }
        }
        public decimal SubTotal
        {
            get { return this.Productos.Sum(i => i.Precio.Value); }
        }
        public decimal SubTotalElectronica
        {
            get { return this.Productos.Where(i => i.Electronica).Sum(i => i.Precio.Value); }
        }
        public decimal SubTotalCalzado
        {
            get { return this.Productos.Where(i => !i.Electronica).Sum(i => i.Precio.Value); }
        }
        public decimal Descuento
        {
            get { return this.Productos.Where(i => i.Descuento.HasValue).Sum(i => i.Descuento.Value); }
        }
        public decimal DescuentoElectronica
        {
            get { return this.Productos.Where(i => i.Electronica && i.Descuento.HasValue).Sum(i => i.Descuento.Value); }
        }
        public decimal DescuentoCalzado
        {
            get { return this.Productos.Where(i => !i.Electronica && i.Descuento.HasValue).Sum(i => i.Descuento.Value); }
        }
        public override decimal Total
        {
            get { return this.SubTotal - this.Descuento; }
        }
        public decimal TotalElectronica
        {
            get { return this.SubTotalElectronica - this.DescuentoElectronica; }
        }
        public decimal TotalCalzado
        {
            get { return this.SubTotalCalzado - this.DescuentoCalzado; }
        }
        public override decimal RemainingElectronica
        { 
            get {
                return this.TotalElectronica - (this.Productos.Where(i => i.Electronica).SelectMany(i => i.FormasPago).Sum(i => i.Importe) ?? 0);
            }
        }
        public override decimal RemainingCalzado
        {
            get
            {
                return this.TotalCalzado - (this.Productos.Where(i => !i.Electronica).SelectMany(i => i.FormasPago).Sum(i => i.Importe) ?? 0);
            }
        }
        public int Unidades
        {
            get { return this.Productos.Count; }
        }
        public bool HasCalzado
        {
            get {
                return this.Productos.Where(i => !i.Electronica).Any();
            }
        }
        #endregion
        #region properties

        //private string _folio;
        //public string Folio
        //{
        //    get { return _folio; }
        //    set { this.Set(nameof(this.Folio), ref _folio, value); }
        //}
        //private int? _clienteId;
        //public int? ClienteId
        //{
        //    get { return _clienteId; }
        //    set { this.Set(nameof(this.ClienteId), ref _clienteId, value); }
        //}

        public ObservableCollection<Models.Producto> Productos { get; set; }
        
        private CollectionViewSource _promocionesCuponesUsadas;
        public ICollectionView PromocionesCuponesUsadas {
            get => _promocionesCuponesUsadas.View;
        }
        public ObservableCollection<Common.Entities.Promocion> PromocionesCupones { get; set; }
        public ObservableCollection<Common.Entities.Cupon> Cupones { get; set; }
        private Models.Producto _selectedItem;
        public Models.Producto SelectedItem
        {
            get { return _selectedItem; }
            set { Set(nameof(this.SelectedItem), ref _selectedItem, value); }
        }        
        private Common.Entities.Cupon _selectedCupon;
        public Common.Entities.Cupon SelectedCupon
        {
            get { return _selectedCupon; }
            set { Set(nameof(this.SelectedCupon), ref _selectedCupon, value); }
        }
        private Common.Entities.Promocion _selectedPromocion;
        public Common.Entities.Promocion SelectedPromocion
        {
            get { return _selectedPromocion; }
            set { Set(nameof(this.SelectedPromocion), ref _selectedPromocion, value); }
        }
        private string _serieSearch;
        public string SerieSearch
        {
            get { return _serieSearch; }
            set { Set(nameof(this.SerieSearch), ref _serieSearch, value); }
        }
        private string _cuponSearch;
        public string CuponSearch
        {
            get { return _cuponSearch; }
            set { Set(nameof(this.CuponSearch), ref _cuponSearch, value); }
        }
        private Empleado _vendedor;
        public Empleado Vendedor
        {
            get { return _vendedor; }
            set { Set(nameof(this.Vendedor), ref _vendedor, value); }
        }
        private SaleResponse _saleResponse;
        public SaleResponse SaleResponse
        {
            get => _saleResponse;
            set => this.Set(nameof(SaleResponse), ref _saleResponse, value);
        }
                
        #endregion

        #region commands
        public RelayCommand PrintCommand { get; private set; }
        public RelayCommand SaleCommand { get; private set; }
        //public h.RelayCommand<FormaPago> AddFormaCommand { get; private set; }
        
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand RemoveCommand { get; private set; }
        public RelayCommand RemovePagoCommand { get; private set; }
        public RelayCommand AddCuponCommand { get; private set; }
        public RelayCommand RemoveCuponCommand { get; private set; }
        public RelayCommand LoadClienteCommand { get; private set; }
        public RelayCommand LoadVendedorCommand { get; private set; }
        public RelayCommand RemoveVendedorCommand { get; private set; }
        public RelayCommand ClearClienteCommand { get; private set; }
        public RelayCommand AddDescuentoAdicional { get; private set; }
        public RelayCommand<Models.MoveDirection> MoveProductoCommand { get; private set; }
        public RelayCommand<Models.MoveDirection> MovePagoCommand { get; private set; }
        public RelayCommand<Models.MoveDirection> MoveCuponCommand { get; private set; }
        public RelayCommand PagarCommand { get; private set; }
        public RelayCommand<string> TestCommand { get; private set; }
        public RelayCommand ConfirmClienteCommand { get; private set; }
        public RelayCommand AddNotaCommand { get; private set; }
        #endregion
    }
}
