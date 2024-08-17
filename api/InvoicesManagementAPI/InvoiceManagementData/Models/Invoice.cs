using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementData.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        public int Id { get; set; }
        public required string PayerName { get; set; }
        public required string InvoiceNumber { get; set; }
        public decimal InvoiceAmount { get; set; }
        public required string InvoiceDocument { get; set; }
        public required string BankSlipDocument { get; set; }
        public int InvoiceStatus { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
