using Azure;
using InvoiceManagementData.Helpers;
using InvoiceManagementData.Interfaces;
using InvoiceManagementData.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;

namespace InvoiceManagementData.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DataContext _context;

        public InvoiceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAll()
        {
            var query = _context.Invoices.AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetFiltered(InvoiceFilters filters)
        {
            var addedFilters = new List<Expression<Func<Invoice, bool>>>();

            if (filters.IssuedMonth.HasValue)
                addedFilters.Add(invoice => invoice.IssueDate.Month == filters.IssuedMonth);

            if (filters.PaymentMonth.HasValue)
                addedFilters.Add(invoice => invoice.PaymentDate.HasValue && invoice.PaymentDate.Value.Month == filters.PaymentMonth);

            if (filters.BillingMonth.HasValue)
                addedFilters.Add(invoice => invoice.BillingDate.HasValue && invoice.BillingDate.Value.Month == filters.BillingMonth);

            if (filters.InvoiceStatus.HasValue)
            {
                if (filters.InvoiceStatus.Value == 3)
                    addedFilters.Add(invoice => invoice.PaymentPromise);
                else
                    addedFilters.Add(invoice => invoice.InvoiceStatus == filters.InvoiceStatus);
            }

            if (filters.IssuedMonth.HasValue)
                addedFilters.Add(invoice => invoice.IssueDate.Month == filters.IssuedMonth);

            if (filters.InvoiceId.HasValue)
                addedFilters.Add(invoice => invoice.Id == filters.InvoiceId);

            if (!string.IsNullOrEmpty(filters.PayerName))
                addedFilters.Add(invoice => invoice.PayerName == filters.PayerName);

            if (!string.IsNullOrEmpty(filters.InvoiceNumber))
                addedFilters.Add(invoice => invoice.InvoiceNumber == filters.InvoiceNumber);

            if (!string.IsNullOrEmpty(filters.InvoiceDocument))
                addedFilters.Add(invoice => invoice.InvoiceDocument == filters.InvoiceDocument);

            if (!string.IsNullOrEmpty(filters.BankSlipDocument))
                addedFilters.Add(invoice => invoice.BankSlipDocument == filters.BankSlipDocument);

            if (filters.StartAt.HasValue && filters.EndAt.HasValue)
            {
                addedFilters.Add(invoice => invoice.IssueDate <= filters.EndAt.Value && invoice.IssueDate >= filters.StartAt.Value);
            }

            var query = _context.Invoices.AsQueryable();

            if (addedFilters.Count > 0)
            {
                foreach (var filter in addedFilters)
                {
                    query = query.Where(filter);
                }
            }

            return await query.ToListAsync();
        }

        public Reports GetReports(DateTime? startAt, DateTime? endAt)
        {
            var query = _context.Invoices.AsQueryable();
            if (startAt.HasValue)
            {
                query = query.Where(invoice => invoice.IssueDate >= startAt.Value);
            }
            if (endAt.HasValue)
            {
                query = query.Where(invoice => invoice.IssueDate <= endAt.Value);
            }

            // Valor total das notas emitidas
            var totalAmoutIssuedInvoice = query.Sum(amount => amount.InvoiceAmount);

            // Valor total das notas emitidas, mas sem ter a cobrança feita
            var totalAmoutWithoutBilling = query.Where(invoice => invoice.BillingDate == null && invoice.PaymentDate == null && !invoice.PaymentPromise)
                                                .Sum(amount => amount.InvoiceAmount);

            // Valor total das notas vencidas - Inadimplência
            var totalAmoutWithPaymentPromise = query.Where(invoice => invoice.PaymentPromise)
                                                    .Sum(amount => amount.InvoiceAmount);

            // Valor total das notas a vencer
            var totalAmoutWithoutPayment = query.Where(invoice => invoice.PaymentDate == null && invoice.BillingDate != null && !invoice.PaymentPromise)
                                                .Sum(amount => amount.InvoiceAmount);

            // Valor total das notas pagas
            var totalAmoutWithPayment = query.Where(invoice => invoice.PaymentDate != null)
                                             .Sum(amount => amount.InvoiceAmount);

            // Gráfico de evolução da inadimplência mês a mês
            var totalCountWithoutPayment = new List<InvoicesMonth>();

            //○ Gráfico de evolução da receita recebida mês a mês;
            var totalCountWithPayment = new List<InvoicesMonth>();

            if (startAt.HasValue && endAt.HasValue)
            {
                if (startAt.Value.Month == endAt.Value.Month)
                {
                    totalCountWithoutPayment = query.Where(invoice => invoice.PaymentDate == null)
                                                        .GroupBy(invoice => new
                                                        {
                                                            invoice.IssueDate.Day,
                                                            invoice.IssueDate.Month,
                                                        })
                                                        .Select(g => new InvoicesMonth
                                                        {
                                                            Day = g.Key.Day,
                                                            Month = g.Key.Month,
                                                            Count = g.Count()
                                                        })
                                                        .OrderBy(result => result.Day)
                                                        .ThenBy(result => result.Month)
                                                        .ToList();

                    totalCountWithPayment = query.Where(invoice => invoice.PaymentDate != null)
                                                .GroupBy(invoice => new
                                                {
                                                    invoice.IssueDate.Month,
                                                    invoice.IssueDate.Day,
                                                })
                                                .Select(g => new InvoicesMonth
                                                {
                                                    Month = g.Key.Month,
                                                    Day = g.Key.Day,
                                                    Count = g.Count()
                                                })
                                                .OrderBy(result => result.Month)
                                                .ToList();
                }

                if (startAt.Value.Month == 1 && endAt.Value.Month == 6)
                {
                    totalCountWithoutPayment = query.Where(invoice => invoice.PaymentDate == null)
                                                        .GroupBy(invoice => new
                                                        {
                                                            invoice.IssueDate.Month,
                                                        })
                                                        .Select(g => new InvoicesMonth
                                                        {
                                                            Month = g.Key.Month,
                                                            Count = g.Count()
                                                        })
                                                        .OrderBy(result => result.Month)
                                                        .ToList();

                    totalCountWithPayment = query.Where(invoice => invoice.PaymentDate != null)
                                                .GroupBy(invoice => new
                                                {
                                                    invoice.IssueDate.Month
                                                })
                                                .Select(g => new InvoicesMonth
                                                {
                                                    Month = g.Key.Month,
                                                    Count = g.Count()
                                                })                                                
                                                .OrderBy(result => result.Month)
                                                .ToList();
                }
            }

            if (totalCountWithoutPayment.Count == 0)
            {
                totalCountWithoutPayment = query.Where(invoice => invoice.PaymentDate == null)
                                                    .GroupBy(invoice => new
                                                    {
                                                        invoice.IssueDate.Month,
                                                        invoice.IssueDate.Year,
                                                    })
                                                    .Select(g => new InvoicesMonth
                                                    {
                                                        Month = g.Key.Month,
                                                        Year = g.Key.Year,
                                                        Count = g.Count()
                                                    })
                                                    .OrderBy(result => result.Year)
                                                    .ThenBy(result => result.Month)
                                                    .ToList();

                totalCountWithPayment = query.Where(invoice => invoice.PaymentDate != null)
                                                .GroupBy(invoice => new
                                                {
                                                    invoice.IssueDate.Month,
                                                    invoice.IssueDate.Year,
                                                })
                                                .Select(g => new InvoicesMonth
                                                {
                                                    Month = g.Key.Month,
                                                    Year = g.Key.Year,
                                                    Count = g.Count()
                                                })
                                                .OrderBy(result => result.Year)
                                                .ThenBy(result => result.Month)
                                                .ToList();
            }            

            var reports = new Reports
            {
                TotalCountWithoutPayment = totalCountWithoutPayment,
                TotalCountWithPayment = totalCountWithPayment,
                TotalAmoutIssuedInvoice = totalAmoutIssuedInvoice,
                TotalAmountPaymentInvoices = totalAmoutWithPayment,
                TotalAmountWithoutBilling = totalAmoutWithoutBilling,
                TotalAmountWithoutPayment = totalAmoutWithoutPayment,
                TotalAmountWithPaymentPromise = totalAmoutWithPaymentPromise,
            };

            return reports;
        }
    }
}
