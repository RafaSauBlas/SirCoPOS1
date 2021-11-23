using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess.SirCoPV
{
    [Table("operaciontarjeta", Schema = "dbo")]
    public class OperacionTarjeta
    {
        [Key]
        public int Id { get; set; }
        public string orderId { get; set; }
        public DateTime? transDate { get; set; }
        public string affiliation { get; set; }
        public string applicationLabel { get; set; }
        public string arqc { get; set; }
        public string aid { get; set; }
        public decimal amount { get; set; }
        public string authCode { get; set; }
        public string bankName { get; set; }
        public string cardExpDate { get; set; }
        public string cardType { get; set; }
        public string cardTypeName { get; set; }
        public string cityName { get; set; }
        public string responseCode { get; set; }
        public string folioNumber { get; set; }
        public bool hasPin { get; set; }
        public string hexSign { get; set; }
        public int isQps { get; set; }
        public string message { get; set; }
        public string moduleCharge { get; set; }
        public string moduleLote { get; set; }
        public string customerName { get; set; }
        public string terminalId { get; set; }
        public string preAuth { get; set; }
        public decimal preStatus { get; set; }
        public string promotion { get; set; }
        public string rePrintDate { get; set; }
        public string rePrintMark { get; set; }
        public string rePrintModule { get; set; }
        public string cardNumber { get; set; }
        public string storeId { get; set; }
        public string storeName { get; set; }
        public string streetName { get; set; }
        public string ticketDate { get; set; }
        public decimal tipAmount { get; set; }
        public decimal tipLessAmount { get; set; }
        public string transType { get; set; }
        public string transactionCertificate { get; set; }
        public string sucursal { get; set; }
        public string venta { get; set; }
        public Guid guid { get; set; }
        public DateTime fum { get; set; }
        public int idformapago { get; set; }
    }
}
