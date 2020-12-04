//using SecuGen.FDxSDKPro.Windows;
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
    /// Interaction logic for ApproveWindow.xaml
    /// </summary>
    public partial class ApproveWindow : Window
    {
        //private SGFingerPrintManager m_FPM;
        //private int m_ImageWidth;
        //private int m_ImageHeight;

        public ApproveWindow()
        {
            InitializeComponent();
            /*
            m_FPM = new SGFingerPrintManager();

            var iError = m_FPM.EnumerateDevice();

            if (m_FPM.NumberOfDevice == 0)
            {
                MessageBox.Show("no devices found");
                return;
            }

            var device_name = SGFPMDeviceName.DEV_AUTO;
            var device_id = (Int32)(SGFPMPortAddr.USB_AUTO_DETECT);

            iError = m_FPM.Init(device_name);
            iError = m_FPM.OpenDevice(device_id);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                SGFPMDeviceInfoParam pInfo = new SGFPMDeviceInfoParam();
                iError = m_FPM.GetDeviceInfo(pInfo);

                m_ImageWidth = pInfo.ImageWidth;
                m_ImageHeight = pInfo.ImageHeight;                
            }
            */
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var proxy = CommonServiceLocator.ServiceLocator.Current.GetInstance<Common.ServiceContracts.IDataServiceAsync>();
            var settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
            var m_RegMin = new Byte[400];
            var res = await this.Prepare(m_RegMin);
            if (!res)
            {
                this.DialogResult = false;
                this.Close();
                return;
            }

            var items = await proxy.ApproveAsync(settings.Sucursal.Clave);
            foreach (var item in items)
            {
                foreach (var h in item.Value)
                {
                    if (Verify(m_RegMin, h))
                    {
                        this.DialogResult = true;
                        this.Close();
                        return;
                    }
                }                
            }            
        }

        private Task<bool> Prepare(byte[] m_RegMin)
        {
            return Task.Run<bool>(() =>
            {
                /*
                //Int32 img_qlty;

                var fp_image = new Byte[m_ImageWidth * m_ImageHeight];
                //img_qlty = 0;

                var iError = m_FPM.GetImage(fp_image);

                //m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);
                //iError = m_FPM.GetImage(fp_image);
                iError = m_FPM.GetImageEx(fp_image, timeout: 10000, dispWnd: 0, quality: 50);

                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    iError = m_FPM.CreateTemplate(fp_image, m_RegMin);

                    if (iError == (Int32)SGFPMError.ERROR_NONE)
                        return true;
                }*/
                return false;
            });
        }

        private bool Verify(byte[] current, byte[] stored)
        {
            /*
            SGFingerPrintManager m_FPM;
            m_FPM = new SGFingerPrintManager();

            Int32 iError;
            bool matched = false;
            SGFPMSecurityLevel secu_level;

            secu_level = SGFPMSecurityLevel.NORMAL;
            iError = m_FPM.MatchTemplate(current, stored, secu_level, ref matched);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                return matched;
            }
            */
            return false;
                //throw new NotSupportedException();
        }
    }
}
