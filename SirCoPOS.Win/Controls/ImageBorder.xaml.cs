using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SirCoPOS.Win.Controls
{
    /// <summary>
    /// Interaction logic for ImageBorder.xaml
    /// </summary>
    public partial class ImageBorder : UserControl
    {
        public ImageBorder()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ImageSourceProperty = 
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageBorder),
                new PropertyMetadata(new PropertyChangedCallback(ImageSourceChangedCallBack)));
        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        static void ImageSourceChangedCallBack(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            var uc = (ImageBorder)property;
            uc.ib.ImageSource = (ImageSource)args.NewValue;
        }
    }
}
