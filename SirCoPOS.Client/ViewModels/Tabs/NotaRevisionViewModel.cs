using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Client.ViewModels.Tabs
{
    class NotaRevisionViewModel : Helpers.TabViewModelBase
    {
        private readonly Common.ServiceContracts.IDataServiceAsync _data;
        private readonly Helpers.ServiceClient _process;
        public NotaRevisionViewModel()
        {
            this.Items = new ObservableCollection<Common.Entities.NoteHeader>();
            this.Details = new ObservableCollection<Common.Entities.NoteDetalle>();
            this.Details.CollectionChanged += Details_CollectionChanged;
            this.LoadCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() =>
            {
                this.Loaded = this.Selected;
                var items = _data.GetNoteDetails(this.Loaded.Id);
                this.Details.Clear();
                foreach (var item in items)
                {
                    this.Details.Add(item);
                }            }, () => this.Selected != null);
            this.SaveCommand = new GalaSoft.MvvmLight.Command.RelayCommand(async () =>
            {
                var item = await _process.RegisterNoteAsync(this.Loaded.Id);
                this.Items.Remove(this.Loaded);
                this.Details.Clear();
                this.Loaded = null;
                MessageBox.Show($"id: {item}");
            }, () => this.Loaded != null);
            this.PropertyChanged += NotaRevisionViewModel_PropertyChanged;
            if (this.IsInDesignMode)
            {
                this.Items.Add(new Common.Entities.NoteHeader { Id = 1, Date = DateTime.Now, Sucursal = "08", CajeroId = 10, Total = 100 });
                this.Items.Add(new Common.Entities.NoteHeader { Id = 1, Date = DateTime.Now, Sucursal = "08", CajeroId = 10, Total = 100 });
                this.Items.Add(new Common.Entities.NoteHeader { Id = 1, Date = DateTime.Now, Sucursal = "08", CajeroId = 10, Total = 100 });
                this.Selected = this.Items[0];
                this.Loaded = this.Items[1];

                this.Details.Add(new Common.Entities.NoteDetalle { Serie = "s1", Amount = 100, Comments = "commnets" });
                this.Details.Add(new Common.Entities.NoteDetalle { Serie = "s1", Amount = 100, Comments = "commnets" });
            }
            else
            {
                _data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
                _process = CommonServiceLocator.ServiceLocator.Current.GetInstance<Helpers.ServiceClient>();

                var items = _data.GetNotes();
                foreach (var item in items)
                {
                    this.Items.Add(item);
                }
            }
        }

        private void Details_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(this.Total));
        }

        private void NotaRevisionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Selected):
                    this.LoadCommand.RaiseCanExecuteChanged();
                    break;
                case nameof(this.Loaded):
                    this.SaveCommand.RaiseCanExecuteChanged();
                    break;
            }
        }

        private Common.Entities.NoteHeader _selected;
        public Common.Entities.NoteHeader Selected
        {
            get => _selected;
            set => this.Set(nameof(Selected), ref _selected, value);
        }
        private Common.Entities.NoteHeader _loaded;
        public Common.Entities.NoteHeader Loaded
        {
            get => _loaded;
            set => this.Set(nameof(Loaded), ref _loaded, value);
        }
        public decimal? Total
        {
            get {
                if (this.Details.Any())
                {
                    var sum = this.Details.Sum(i => i.Amount);
                    return sum;
                }
                return null;
            }
        }
        public ObservableCollection<Common.Entities.NoteHeader> Items { get; set; }
        public ObservableCollection<Common.Entities.NoteDetalle> Details { get; set; }
        public GalaSoft.MvvmLight.Command.RelayCommand LoadCommand { get; private set; }
        public GalaSoft.MvvmLight.Command.RelayCommand SaveCommand { get; private set; }
    }
}
