using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SirCoPOS.Win.Helpers
{
    class CallbackHandler //: Common.ServiceContracts.IServiceDuplexCallback
    {
        public void Receive(string msg)
        {
            MessageBox.Show($"Receive: {msg}");
        }

        public void Response(bool result)
        {
            MessageBox.Show($"Response: {result}");
        }

        public void Update(Guid gid, bool complete)
        {
            MessageBox.Show($"Update: {gid}, complete: {complete}");
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(
                new Messages.UpdateStatus { GID = gid, Complete = complete });
        }
    }
}
