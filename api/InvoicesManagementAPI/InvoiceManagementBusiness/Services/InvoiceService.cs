using InvoiceManagementBusiness.Intefaces;
using InvoiceManagementData.Models;
using InvoiceManagementData.Interfaces;

namespace InvoiceManagementBusiness.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IEnumerable<Invoice>> GetAll(DateTime? startAt, DateTime? finishAt)
        {
            return await _invoiceRepository.GetAll(startAt, finishAt);
        }
    }
}
