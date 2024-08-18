using InvoiceManagementApp.Enums;
using InvoiceManagementApp.Models;
using InvoiceManagementApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceManagementApp.Pages
{
    public class InvoiceModel : PageModel
    {
        private readonly ILogger<InvoiceModel> _logger;
        private readonly InvoiceService _invoiceService;

        public InvoiceModel(ILogger<InvoiceModel> logger, InvoiceService invoiceService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
        }

        public string? ErrorMessage
        {
            get; set;
        }
        public PagedInvoices PagedInvoices
        {
            get; set;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var queryString = Request.QueryString.ToString();

                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    queryString = queryString.Replace("?", "");
                }

                PagedInvoices = await _invoiceService.GetInvoiceList(queryString);
            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = $"Erro ao buscar os dados da API: ${ex.Message}";
            }
        }

        public async Task OnPostAsync(string typeFilter, string valueFilter, string filterDescription)
        {
            try
            {
                string queryParams = null;

                TempData["FilterDescription"] = filterDescription;



                if (!string.IsNullOrEmpty(typeFilter))
                {
                    string value;
                    if (typeFilter == "InvoiceStatus")
                    {
                        Enum.TryParse(valueFilter, out InvoiceStatus status);
                        value = $"{(int)status}";
                    }
                    else
                    {
                        value = valueFilter;
                    }

                    queryParams = $"{typeFilter}={value}";
                }

                PagedInvoices = await _invoiceService.GetInvoiceList(queryParams);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
    }
}
