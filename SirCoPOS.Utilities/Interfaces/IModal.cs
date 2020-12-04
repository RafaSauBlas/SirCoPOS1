using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SirCoPOS.Utilities.Interfaces
{
    public interface IModal
    {        
        string Title { get; }
        bool CloseTab { get; }
        Guid GID { get; set; }
    }

    public interface IModalItem/*<T>*/ : IModal 
        //where T : Utilities.Interfaces.IProducto
    {
        //T Item { get; set; }
        Utilities.Interfaces.IProducto Item { get; set; }
    }
    public interface IModalDevolucionItem : IModal
    { 
        Common.Entities.ProductoDevolucion Item { get; set; }
    }
}
