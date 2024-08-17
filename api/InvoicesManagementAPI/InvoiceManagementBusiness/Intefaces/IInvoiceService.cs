using InvoiceManagementData.Models;

namespace InvoiceManagementBusiness.Intefaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAll(DateTime? startAt = null, DateTime? finishAt = null);
    }
}
