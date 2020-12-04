using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web.Areas.Admin.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
    }
}