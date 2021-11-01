using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using GalaSoft.MvvmLight.Messaging;
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

namespace SirCoPOS.Client.Views.Tabs
{
    /// <summary>
    /// Interaction logic for FondoApertura.xaml
    /// </summary>
    [Utilities.Extensions.ExportView]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Utilities.Extensions.MetadataTab(Utilities.Constants.TabType.FondoApertura)]
    public partial class FondoApertura : UserControl, Utilities.Interfaces.ITabView
    {
        public FondoApertura()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, "FocusAuditor", FocusAuditor);
            Messenger.Default.Register<string>(this, "FocusBoton", FocusBoton);
        }

        public void FocusBoton(string msg)
        {
            if (msg == "focusbtn")
            {
                this.btn_aceptar.Focus();
            }
        }

        public void Init()
        {
            this.txt_importe.Text = "0";

            ViewModels.Tabs.FondoAperturaViewModel data = this.DataContext as ViewModels.Tabs.FondoAperturaViewModel;
            if (data != null)
            {
                if (data.Cajero.Depto == (int)Common.Constants.Departamento.ADM || data.Cajero.Depto == (int)Common.Constants.Departamento.SIS)
                {
                    this.lbl_Auditor.Visibility = System.Windows.Visibility.Hidden;
                    this.txt_audid.Visibility = System.Windows.Visibility.Hidden;
                    this.nom_auditor.Visibility = System.Windows.Visibility.Hidden;
                    this.lbl_contra.Visibility = System.Windows.Visibility.Hidden;
                    this.txtB_Contra.Visibility = System.Windows.Visibility.Hidden;
                    
                }
                
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }

        public void FocusAuditor(string msg)
        {
            if (msg == "focus")
            {
                this.txt_audid.Focus();
                this.txt_audid.SelectAll();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
