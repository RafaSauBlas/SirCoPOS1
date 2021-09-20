using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cmd = GalaSoft.MvvmLight.Command;
using SirCoPOS.Utilities.Interfaces;
using SirCoPOS.Utilities.Models;

namespace SirCoPOS.Client.Helpers
{
    public abstract class ModalViewModelBase<T> : Utilities.Helpers.EntityBase, Utilities.Interfaces.IModalViewModel
    {
        public ModalViewModelBase()
        {
            this.AcceptCommand = new cmd.RelayCommand(this.AcceptHelper, this.CanAccept);
            this.CancelCommand = new cmd.RelayCommand(this.CancelHelper);
        }
        private async void AcceptHelper()
        {
            var res = await this.Prepare(true);
            this.Accept(res);
        }

        private async void CancelHelper()
        {
            await this.Prepare(false);
            this.Cancel();
        }
        protected virtual Task<T> Prepare(bool acept) => Task.FromResult(default(T));
        protected abstract void Accept(T data);
        protected virtual bool CanAccept()
        {
            return base.IsValid();
        }
        protected virtual void Cancel()
        {
            Messenger.Default.Send(
                new Utilities.Messages.ModalResponse { Success = false },
                this.GID);
        }
        public Guid GID { get; set; }
        public cmd.RelayCommand AcceptCommand { get; private set; }
        public cmd.RelayCommand CancelCommand { get; private set; }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(nameof(this.IsBusy), ref _isBusy, value); }
        }

        protected void RegisterMessages()
        {
            Messenger.Default.Register<Messages.Vendedor>(this, this.GID, m => {
                Messenger.Default.Send<string>("focus", "Focusear");
            });
        }

    }

    public abstract class ModalViewModelBase : ModalViewModelBase<object>
    {
        protected abstract void Accept();
        protected override void Accept(object data)
        {
            this.Accept();
        }
    }
}
