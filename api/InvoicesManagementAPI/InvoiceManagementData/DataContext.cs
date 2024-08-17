using InvoiceManagementData.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagementData
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Invoice> Invoices { get; set; }
    }
}
