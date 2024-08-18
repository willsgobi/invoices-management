using InvoiceManagementData.Helpers;
using InvoiceManagementData.Models;

namespace InvoiceManagementData.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAll();
        Task<IEnumerable<Invoice>> GetFiltered(InvoiceFilters filters);
        Reports GetReports(DateTime? startAt, DateTime? endAt);
    }
}
