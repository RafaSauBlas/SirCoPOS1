using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Interfaces
{
    public interface IScanner
    {
        void Start();
        void Stop();
        event EventHandler<string> DataReceived;        
    }
}
