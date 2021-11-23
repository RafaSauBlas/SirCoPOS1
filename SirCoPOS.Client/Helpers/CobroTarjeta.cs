using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using Newtonsoft.Json;
using NetPayConnect;

namespace SirCoPOS.Client.Helpers
{
    public class CobroTarjeta
    {

        NetPayConnect.Operator connectorPAX = new NetPayConnect.Operator();
        private static bool disablePrint = false;
        public void Cobrar(double pagar, string folio)
        {
            try
            {
                connectorPAX.SetAmount(pagar);
                connectorPAX.SetFolio(folio);
                connectorPAX.SetTip(0.0);
                connectorPAX.SetMSI(Convert.ToInt32(0));
                connectorPAX.setDisablePrintAnimation(disablePrint);
                connectorPAX.sendData();
            }
            catch (Exception ec)
            {
                var msg = ec.Message;
                MessageBox.Show(msg, "Cobro Tarjeta", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public string getResponseData()
        {
            connectorPAX.ReceiveData();
            if (connectorPAX.getResponse() != null)
            {
                return JsonConvert.SerializeObject(connectorPAX.getResponse());
            }
            else
            {
                return null ;
            }
        }

        public string Cancela(string orderId)
        {
            string result = null;
            try
            {
                result = connectorPAX.sendCancel(orderId);
            }
            catch (Exception ec)
            {
                result = null;
            }
            getResponseData();
            return result;
        }

        public string Reimprime(string orderId)
        {
            string result = null;
            try
            {
                result = connectorPAX.sendPrint(orderId);
            }
            catch (Exception ec)
            {
                result = null;
            }
            getResponseData();
            return result;
        }

    }
}