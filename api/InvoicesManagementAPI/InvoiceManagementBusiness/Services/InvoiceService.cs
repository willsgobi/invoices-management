using InvoiceManagementBusiness.Intefaces;
using InvoiceManagementData.Models;
using InvoiceManagementData.Interfaces;
using InvoiceManagementData.Helpers;

namespace InvoiceManagementBusiness.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IEnumerable<Invoice>> GetAll()
        {
            return await _invoiceRepository.GetAll();
        }

        public async Task<IEnumerable<Invoice>> GetFiltered(InvoiceFilters filters)
        {
            return await _invoiceRepository.GetFiltered(filters);
        }

        public async Task<Reports> GetReports(DateTime? startAt, DateTime? endAt)
        {
            return _invoiceRepository.GetReports(startAt, endAt);
        }
    }
}
