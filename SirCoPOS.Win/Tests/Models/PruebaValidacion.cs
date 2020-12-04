using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Tests.Models
{
    class PruebaValidacion : Utilities.Helpers.EntityBase
    {
        private string _campo1;
        [Required]
        public string Campo1 {
            get { return _campo1; }
            set {
                this.Set(nameof(this.Campo1), ref _campo1, value);
            }
        }
        [Required]
        public string Campo2 {
            get { return this.GetValue<string>(nameof(this.Campo2)); }
            set { this.SetValue(nameof(this.Campo2), value); }
        }
        [Required]
        public string Campo3 {
            get { return this.GetValue<string>(nameof(this.Campo3)); }
            set { this.SetValue(nameof(this.Campo3), value); }
        }
    }
}
