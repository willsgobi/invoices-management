using InvoiceManagementData.Models;
using InvoiceManagementData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagementData.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DataContext _context;

        public InvoiceRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Invoice>> GetAll(DateTime? startAt, DateTime? endAt)
        {
            return await _context.Invoices.ToListAsync();
        }
    }
}
