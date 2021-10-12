using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetPayConnect;

namespace SirCoPOS.Client.Helpers
{
    public class CobroTarjeta
    {

        public static string Venta(decimal cantidad, string folio)
        {
            Operator connectorPAX = new Operator();

            connectorPAX.SetAmount((double)cantidad);
            connectorPAX.SetFolio(folio);
            //connectorPAX.SetMSI(msi);
            //enviamos la transacción y la almacenamos en la variable result string
            string result = connectorPAX.sendData();

            return result;
        }
        public static string Reimpresion(string orderId)
        {
            Operator connectorPAX = new Operator();
            string result = connectorPAX.sendPrint(orderId);

            return result;
        }

        public static string Cancelacion(string orderId)
        {
            Operator connectorPAX = new Operator();
            string result = connectorPAX.sendCancel(orderId);

            return result;
        }

        public static NetPayConnect.Response GetResponse()
        {
            Operator connectorPAX = new Operator();
            NetPayConnect.Response result = connectorPAX.getResponse();

            return result;
        }

    }
}