using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SirCoPOS.Common.Entities;
using SirCoPOS.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Helpers
{
    public abstract class TabViewModelBase : Utilities.Helpers.ViewModelBase, Utilities.Interfaces.ITabViewModel
    {
        private Utilities.Models.Settings _settings;
        public TabViewModelBase()
        {
            if (!this.IsInDesignMode)
                _settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
            this.PropertyChanged += TabViewModelBase_PropertyChanged;
            this.CloseCommand = new RelayCommand(() => {

                Messenger.Default.Send(new Utilities.Messages.CloseTab { GID = this.GID });

            }, () => this.IsComplete);
        }

        private void TabViewModelBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.IsComplete):
                    this.CloseCommand.RaiseCanExecuteChanged();
                    break;
            }
        }

        public Sucursal Sucursal => _settings?.Sucursal;
        public Empleado Cajero => _settings?.Cajero;
        private Guid _guid;
        public Guid GID {
            get => _guid;
            protected set {
                if (_guid == Guid.Empty)
                    _guid = value;
                else
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();
                    _guid = value;
                }
            }
        }
        public Utilities.Constants.TabType TabType { get; set; }

        public void Init(Guid gid)
        {
            Console.WriteLine($"initvm: {gid}");
            this.GID = gid;
            Messenger.Default.Register<Utilities.Messages.CloseTab>(this, gid,
                m => {
                    this.Close();
                });
            this.RegisterMessages();
            this.LoadData();
        }
        protected virtual void RegisterMessages() { }
        protected virtual void LoadData() { }
        public virtual void Close() { }
        public RelayCommand CloseCommand { get; private set; }
        protected virtual bool IsReady()
        {
            return _complete;
        }
        public bool IsComplete
        {
            get => this.IsReady();
        }
        private bool _complete;
        protected void Complete()
        {
            _complete = true;
            this.RaisePropertyChanged(nameof(this.IsComplete));
        }
    }
}
