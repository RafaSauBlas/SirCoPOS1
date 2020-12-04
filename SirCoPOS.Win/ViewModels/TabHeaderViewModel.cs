using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.ViewModels
{
    class TabHeaderViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        public TabHeaderViewModel()
        {
            this.CloseCommand = new RelayCommand(() => {
                Console.WriteLine($"closing: {this.GID}");
                Messenger.Default.Send(new Utilities.Messages.CloseTab { GID = this.GID });
            });
            if (this.IsInDesignMode)
            {
                this.Title = "title";
            }
        }
        public Guid GID { get; set; }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { this.Set(nameof(this.Title), ref _title, value); }
        }
        public RelayCommand CloseCommand { get; private set; }
    }
}
