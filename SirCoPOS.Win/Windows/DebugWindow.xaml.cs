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
using System.Windows.Shapes;

namespace SirCoPOS.Win.Windows
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public DebugWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.comboBoxCall.SelectedIndex == -1)
                return;

            var txt = this.textBox.Text;
            var process = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IProcessServiceAsync>();
            var data = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();

            var val = (string)this.comboBoxCall.SelectionBoxItem;
            MessageBox.Show($"sending... {val}");
            switch (val)
            {
                case "Sale":
                    {
                        var item = Utilities.Helpers.Serializer.Deserialize<Helpers.Debug.Sale>(txt);
                        var res = process.Sale(item.Item);
                    }
                    break;
                case "CheckPromociones":
                    {
                        var item = Utilities.Helpers.Serializer.Deserialize<Helpers.Debug.CheckPromociones>(txt);
                        var res = data.CheckPromociones(item.Request);
                    }
                    break;
                case "Return":
                    {
                        var item = Utilities.Helpers.Serializer.Deserialize<Helpers.Debug.Return>(txt);
                        var res = process.Return(item.Item);
                    }
                    break;
                default:
                    MessageBox.Show($"Not Supported: {val}");
                    break;
            }
        }
    }
}
