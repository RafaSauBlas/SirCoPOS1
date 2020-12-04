using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Helpers
{
    class ScannerHelper : Utilities.Interfaces.IScanner
    {
        private readonly SerialPort _serial;
        public ScannerHelper()
        {
            var settings = CommonServiceLocator.ServiceLocator.Current.GetInstance<Utilities.Models.Settings>();
            var com = settings.Scanner;

            if (com == null)
                return;

            _serial = new SerialPort();
            _serial.PortName = com;
            _serial.BaudRate = 115200;
            _serial.Parity = Parity.None;
            _serial.DataBits = 8;
            _serial.StopBits = StopBits.One;
            _serial.Handshake = Handshake.None;            
            _serial.DataReceived += Serial_DataReceived;
        }
        public void Start()
        {
            if(!_serial.IsOpen)
                _serial.Open();
        }
        public void Stop()
        {
            if (_serial.IsOpen)
                _serial.Close();
        }
        public event EventHandler<string> DataReceived;
        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serial = (SerialPort)sender;
            var cmd = serial.ReadExisting();
            this.DataReceived?.Invoke(this, cmd);
        }
    }
}
