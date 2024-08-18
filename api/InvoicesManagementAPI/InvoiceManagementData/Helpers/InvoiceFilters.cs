namespace InvoiceManagementData.Helpers
{
    public class InvoiceFilters
    {        
        public int? IssuedMonth
        {
            get; set;
        }
        public int? BillingMonth
        {
            get; set;
        }
        public int? PaymentMonth
        {
            get; set;
        }
        public int? InvoiceStatus
        {
            get; set;
        }
        public int? InvoiceId
        {
            get; set;
        }
        public string? PayerName
        {
            get; set;
        }
        public string? InvoiceNumber
        {
            get; set;
        }
        public string? InvoiceDocument
        {
            get; set;
        }
        public string? BankSlipDocument
        {
            get; set;
        }
        public int? Page { get; set; }
        public DateTime? StartAt
        {
            get; set;
        }
        public DateTime? EndAt
        {
            get; set;
        }
    }
}
