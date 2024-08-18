using InvoiceManagementData.Helpers;
using InvoiceManagementData.Models;

namespace InvoiceManagementData.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAll();
        Task<PagedItems> GetFiltered(InvoiceFilters filters);
        Reports GetReports(DateTime? startAt, DateTime? endAt);
    }
}
