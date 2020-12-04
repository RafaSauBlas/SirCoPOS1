using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.Common.Helpers;

namespace SirCoPOS.Common
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void Set<T>(string propertyName, ref T currentValue, T newValue)
        {
            if (currentValue.IsDefault())
            {
                if (newValue.IsDefault())
                    return;
            }
            else if (!newValue.IsDefault() && currentValue.IsEquals(newValue))
                return;

            currentValue = newValue;

            RaisePropertyChanged(propertyName);
        }
    }
}
