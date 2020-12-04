using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirCoPOS.Server.Common.Extensions
{
    public class LogMessageItem
    {
        public string Action { get; set; }
        public DateTime StartDate { get; set; }
        public string Request { get; set; }        
    }
}