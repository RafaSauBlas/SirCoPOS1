using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Win.Helpers
{
    static class LocatorService
    {
        public static void SetViewModel(DependencyObject element, Type value)
        {
            element.SetValue(ViewModelProperty, value);
        }
        public static Type GetViewModel(DependencyObject element)
        {
            return (Type)element.GetValue(ViewModelProperty);
        }

        public static readonly DependencyProperty ViewModelProperty =
          DependencyProperty.RegisterAttached("ViewModel",
          typeof(Type), typeof(LocatorService),
          new PropertyMetadata(new PropertyChangedCallback(OnViewModelPropertyChanged)));

        private static void OnViewModelPropertyChanged(DependencyObject element,
                          DependencyPropertyChangedEventArgs args)
        {
            var vm = GetViewModel(element);
            if (element is FrameworkElement)
            {
                var fe = (FrameworkElement)element;
                fe.DataContext = NinjectBootstrapper.Current.GetInstance(vm);
            }
        }
    }
}
