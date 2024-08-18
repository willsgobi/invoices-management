using InvoiceManagementApp.Models;
using InvoiceManagementApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace InvoiceManagementApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly InvoiceService _invoiceService;
        private readonly ILogger<IndexModel> _logger;

        public string? ErrorMessage { get; set; }
        public Reports Reports
        {
            get; set;
        }

        public FilterModel FilterModel { get; set; }


        public IndexModel(ILogger<IndexModel> logger, InvoiceService httpClientService)
        {
            _invoiceService = httpClientService;

            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                Reports = await _invoiceService.GetReportsAsync();
            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = $"Erro ao buscar os dados da API: ${ex.Message}";
            }
        }

        public async Task OnPostAsync(string typeFilter, string? filterMonth, string? filterQuarter, string? filterYear)
        {
            try
            {
                DateTime? startAt = null;
                DateTime? endAt = null;
                TempData["FilterType"] = typeFilter;

                if (typeFilter == "mensal" && !string.IsNullOrWhiteSpace(filterMonth))
                {
                    var year = DateTime.Now.Year - 1;
                    var month = int.Parse(filterMonth);
                    var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year - 1, month);
                    startAt = new DateTime(year, month, 1);
                    endAt = new DateTime(year, month, daysInMonth);
                    TempData["FilterValue"] = filterMonth;
                }

                if (typeFilter == "trimestral" && !string.IsNullOrWhiteSpace(filterQuarter))
                {
                    if (filterQuarter == "1")
                    {
                        var year = DateTime.Now.Year - 1;
                        var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year - 1, 3);

                        startAt = new DateTime(year, 1, 1);
                        endAt = new DateTime(year, 3, daysInMonth);
                    }
                    else if (filterQuarter == "2")
                    {
                        var year = DateTime.Now.Year - 1;
                        var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year - 1, 6);

                        startAt = new DateTime(year, 1, 4);
                        endAt = new DateTime(year, 6, daysInMonth);
                    }
                    else if (filterQuarter == "3")
                    {
                        var year = DateTime.Now.Year - 1;
                        var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year - 1, 9);

                        startAt = new DateTime(year, 1, 7);
                        endAt = new DateTime(year, 9, daysInMonth);
                    }
                    else
                    {
                        var year = DateTime.Now.Year - 1;
                        var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year - 1, 12);

                        startAt = new DateTime(year, 10, 1);
                        endAt = new DateTime(year, 12, daysInMonth);
                    }
                    TempData["FilterValue"] = filterQuarter;
                }

                if (typeFilter == "anual" && !string.IsNullOrWhiteSpace(filterYear))
                {
                    var year = int.Parse(filterYear);
                    var daysInMonth = DateTime.DaysInMonth(year, 12);
                    startAt = new DateTime(year, 1, 1);
                    endAt = new DateTime(year, 12, daysInMonth);

                    TempData["FilterValue"] = filterYear;
                }

                DefineFiltersMessage(TempData["FilterType"].ToString(), TempData["FilterValue"].ToString());

                Reports = await _invoiceService.GetReportsAsync(startAt, endAt);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private void DefineFiltersMessage(string? filterType, string? filterValue)
        {
            if (filterType != null && filterValue != null)
            {
                FilterModel = new FilterModel();

                FilterModel.FilterType = filterType;

                if (filterType == "mensal")
                {
                    FilterModel.FilterValue = @DateTimeFormatInfo.GetInstance(new CultureInfo("pt-BR")).GetMonthName(int.Parse(filterValue));
                }
                else if (filterType == "semestral")
                {
                    FilterModel.FilterValue = filterValue == "1" ? "primeiro" : "segundo";
                }
                else
                {
                    FilterModel.FilterValue = filterValue;
                }
            }
        }
    }
}
