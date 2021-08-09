using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Models
{
    public class NuevoCliente : Utilities.Helpers.EntityBase
    {
        public int? Id { get; set; }
        [Required]
        public string Nombre
        {
            get { return this.GetValue<string>(nameof(this.Nombre)); }
            set { this.SetValue(nameof(this.Nombre), value); }
        }
        [Required]
        public string ApPaterno
        {
            get { return this.GetValue<string>(nameof(this.ApPaterno)); }
            set { this.SetValue(nameof(this.ApPaterno), value); }
        }
        [Required]
        public string ApMaterno
        {
            get { return this.GetValue<string>(nameof(this.ApMaterno)); }
            set { this.SetValue(nameof(this.ApMaterno), value); }
        }
        [Required]
        public string CodigoPostal
        {
            get { return this.GetValue<string>(nameof(this.CodigoPostal)); }
            set { this.SetValue(nameof(this.CodigoPostal), value); }
        }
        public Common.Entities.Colonia Colonia
        {
            get { return this.GetValue<Common.Entities.Colonia>(nameof(this.Colonia)); }
            set { this.SetValue(nameof(this.Colonia), value); }
        } 
        public string Celular
        {
            get { return this.GetValue<string>(nameof(this.Celular)); }
            set { this.SetValue(nameof(this.Celular), value); }
        }
        [Required]
        public string Celular1
        {
            get { return this.GetValue<string>(nameof(this.Celular1)); }
            set { this.SetValue(nameof(this.Celular1), value); }
        }
        [Required]
        public string Calle
        {
            get { return this.GetValue<string>(nameof(this.Calle)); }
            set { this.SetValue(nameof(this.Calle), value); }
        }
        public short Numero
        {
            get { return this.GetValue<short>(nameof(this.Numero)); }
            set { this.SetValue(nameof(this.Numero), value); }
        }
        public string Referencia
        {
            get { return this.GetValue<string>(nameof(this.Referencia)); }
            set { this.SetValue(nameof(this.Referencia), value); }
        }
        public string Email
        {
            get { return this.GetValue<string>(nameof(this.Email)); }
            set { this.SetValue(nameof(this.Email), value); }
        }
        [Required]
        public string Sexo
        {
            get { return this.GetValue<string>(nameof(this.Sexo)); }
            set { this.SetValue(nameof(this.Sexo), value); }
        }
        public int Idusuario
        {
            get { return this.GetValue<int>(nameof(this.Idusuario)); }
            set { this.SetValue(nameof(this.Idusuario), value); }
        }
    }
}
