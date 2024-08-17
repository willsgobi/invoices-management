using InvoiceManagementData.Models;

namespace InvoiceManagementData.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAll(DateTime? startAt, DateTime? endAt);
    }
}
