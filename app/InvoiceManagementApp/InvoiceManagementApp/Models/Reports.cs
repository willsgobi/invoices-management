namespace InvoiceManagementApp.Models
{
    public class Reports
    {
        public decimal TotalAmoutIssuedInvoice
        {
            get; set;
        }
        public decimal TotalAmountWithoutBilling
        {
            get; set;
        }
        public decimal TotalAmountWithPaymentPromise
        {
            get; set;
        }
        public decimal TotalAmountWithoutPayment
        {
            get; set;
        }
        public decimal TotalAmountPaymentInvoices
        {
            get; set;
        }
        public List<InvoicesMonth> TotalCountWithoutPayment { get; set; } = [];
        public List<InvoicesMonth> TotalCountWithPayment { get; set; } = [];
    }
}
