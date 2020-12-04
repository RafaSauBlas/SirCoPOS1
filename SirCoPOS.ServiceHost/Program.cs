using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var sh = new System.ServiceModel.ServiceHost(typeof(LocalService)))
            using (var sh = new System.ServiceModel.ServiceHost(typeof(ServiceDuplex)))
            {
                Console.WriteLine("openning...");
                sh.Open();
                Console.WriteLine("ready");
                Console.ReadLine();
                Console.WriteLine("closing...");
                sh.Close();
            }
        }
    }
}
