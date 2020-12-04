using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class DescuentoEspecialViewModel : Helpers.ModalViewModelBase, Utilities.Interfaces.IModal
    {
        public string Title => "Descuento Especial";
        private readonly Common.ServiceContracts.IDataServiceAsync _proxy;
        public DescuentoEspecialViewModel()
        {
            this.PropertyChanged += DescuentoEspecialViewModel_PropertyChanged;
            if (!this.IsInDesignMode)
            {
                _proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                this.Items = _proxy.GetDescuentoAdicionals();
            }            
            if (this.IsInDesignMode)
            {
                this.Descripcion = "desc";
                this.Items = new Common.Entities.DescuentoAdicional[] 
                {
                    new Common.Entities.DescuentoAdicional { Id = 1, Descripcion = "r1", Descuento = .1m },
                    new Common.Entities.DescuentoAdicional { Id = 2, Descripcion = "r2", Descuento = .2m }
                };
                this.SelectedItem = this.Items.First();
            }
        }

        private void DescuentoEspecialViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.SelectedItem):
                case nameof(this.Descripcion):
                    this.AcceptCommand.RaiseCanExecuteChanged();
                    break;
            }            
        }

        protected override bool CanAccept()
        {
            return this.SelectedItem != null && !String.IsNullOrEmpty(this.Descripcion);
        }
        protected override void Accept()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(                
                new Messages.DescuentoEspecial {
                    Descripcion = this.Descripcion, 
                    Descuento = this.SelectedItem, 
                    Success = true
                }, this.GID);
        }

        #region properties
        private Common.Entities.DescuentoAdicional _SelectedItem;
        public Common.Entities.DescuentoAdicional SelectedItem
        {
            get { return _SelectedItem; }
            set { Set(nameof(this.SelectedItem), ref _SelectedItem, value); }
        }

        private IEnumerable<Common.Entities.DescuentoAdicional> _Items;
        public IEnumerable<Common.Entities.DescuentoAdicional> Items
        {
            get { return _Items; }
            set { Set(nameof(this.Items), ref _Items, value); }
        }

        private string _descripcion;
        public string Descripcion
        {
            get => _descripcion;
            set => this.Set(nameof(this.Descripcion), ref _descripcion, value);
        }
        public bool CloseTab => false;
        #endregion
    }
}
