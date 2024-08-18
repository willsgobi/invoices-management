namespace InvoiceManagementData.Models
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
        public required List<InvoicesMonth> TotalCountWithoutPayment
        {
            get; set;
        }
        public required List<InvoicesMonth> TotalCountWithPayment
        {
            get; set;
        }
    }

    public class InvoicesMonth
    {
        public int Month { get; set; }
        public int Year
        {
            get; set;
        }
        public int Count { get; set; }
    }
}
