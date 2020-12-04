using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Interfaces
{
    public interface IModalViewModel
    {
        RelayCommand AcceptCommand { get; }
        RelayCommand CancelCommand { get; }
    }
}
