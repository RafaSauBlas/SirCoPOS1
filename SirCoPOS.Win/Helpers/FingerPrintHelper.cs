using SecuGen.FDxSDKPro.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Win.Helpers
{
    class FingerPrintHelper
    {
        public FingerPrintHelper()
        {
            SecuGen.Loader.LoadAssemblies();
            m_FPM = new SGFingerPrintManager();
        }
        private SGFingerPrintManager m_FPM;
        private int m_ImageWidth;
        private int m_ImageHeight;
        public bool Connect()
        {
            var iError = m_FPM.EnumerateDevice();

            if (m_FPM.NumberOfDevice == 0)
            {
                MessageBox.Show("no devices found");
                return false;
            }

            iError = m_FPM.Init(SGFPMDeviceName.DEV_AUTO);
            iError = m_FPM.OpenDevice((int)SGFPMPortAddr.USB_AUTO_DETECT);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                SGFPMDeviceInfoParam pInfo = new SGFPMDeviceInfoParam();
                iError = m_FPM.GetDeviceInfo(pInfo);

                m_ImageWidth = pInfo.ImageWidth;
                m_ImageHeight = pInfo.ImageHeight;

                m_FPM.SetTemplateFormat(SGFPMTemplateFormat.SG400);

                return true;
            }
            return false;            
        }
        public bool Close()
        {
            var iError = m_FPM.CloseDevice();
            if (iError == (Int32)SGFPMError.ERROR_NONE)
                return true;
            return false;
        }
        public async Task<byte[]> Scan()
        {
            var m_RegMin = new Byte[400];
            var valid = await Prepare(m_RegMin);
            if (valid)
            {
                return m_RegMin;
            }
            return null;
        }
        private Task<bool> Prepare(byte[] m_RegMin)
        {
            return Task.Run<bool>(() =>
            {
                var fp_image = new Byte[m_ImageWidth * m_ImageHeight];
                
                var iError = m_FPM.GetImage(fp_image);

                iError = m_FPM.GetImageEx(fp_image, timeout: 10000, dispWnd: 0, quality: 50);

                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    iError = m_FPM.CreateTemplate(fp_image, m_RegMin);

                    if (iError == (Int32)SGFPMError.ERROR_NONE)
                        return true;
                }
                return false;
            });
        }
        public bool Verify(byte[] current, byte[] stored)
        {
            int iError;
            bool matched = false;

            iError = m_FPM.MatchTemplate(current, stored, SGFPMSecurityLevel.NORMAL, ref matched);

            if (iError == (int)SGFPMError.ERROR_NONE)
            {
                return matched;
            }
            else
                throw new NotSupportedException();
        }
    }
}
