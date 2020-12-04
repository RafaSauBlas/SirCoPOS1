using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Helpers
{
    public class ViewModelBase : EntityBase //GalaSoft.MvvmLight.ViewModelBase
    {
        //public string User {
        //    get {
        //        return Thread.CurrentPrincipal.Identity.Name;
        //    }
        //}
        #region properties
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(nameof(this.IsBusy), ref _isBusy, value); }
        }
        #endregion
    }
}
