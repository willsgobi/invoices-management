namespace InvoiceManagementData.Models
{
    public class PagedItems
    {
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
