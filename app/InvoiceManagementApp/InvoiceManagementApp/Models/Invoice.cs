using InvoiceManagementApp.Enums;
using System.ComponentModel;

namespace InvoiceManagementApp.Models
{
    public class Invoice
    {
        public int Id
        {
            get; set;
        }
        [DisplayName("Nome")]
        public required string PayerName
        {
            get; set;
        }
        [DisplayName("Número da nota")]
        public required string InvoiceNumber
        {
            get; set;
        }
        [DisplayName("Valor")]
        public decimal InvoiceAmount
        {
            get; set;
        }
        [DisplayName("Documento da nota")]
        public required string InvoiceDocument
        {
            get; set;
        }
        [DisplayName("Documento do boleto")]
        public required string BankSlipDocument
        {
            get; set;
        }
        [DisplayName("Status")]
        public InvoiceStatus InvoiceStatus
        {
            get; set;
        }
        [DisplayName("Promessa de pagamento")]
        public bool PaymentPromise
        {
            get; set;
        }
        [DisplayName("Emitido em")]
        public DateTime IssueDate
        {
            get; set;
        }
        [DisplayName("Cobrado em")]
        public DateTime? BillingDate
        {
            get; set;
        }
        [DisplayName("Pago em")]
        public DateTime? PaymentDate
        {
            get; set;
        }
    }
}
