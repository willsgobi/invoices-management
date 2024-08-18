using InvoiceManagementData.Helpers;
using InvoiceManagementData.Models;

namespace InvoiceManagementBusiness.Intefaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAll();
        Task<PagedItems> GetFiltered(InvoiceFilters filters);
        Task<Reports> GetReports(DateTime? startAt, DateTime? endAt);
    }
}
