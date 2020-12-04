using SirCoPOS.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SirCoPOS.Client.Models
{
    public class FormaPagoKey
    {
        public Key? Key { get; set; }
        public bool Enabled { get; set; }
        public string KeyStr => this.Key?.ToString();
        public bool Duplicate { get; set; }
        public bool WithClient { get; set; }
        public bool ClientRequired { get; set; }
        public bool Credito { get; set; }
    }
}
