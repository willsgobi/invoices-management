using InvoiceManagementApp.Models;

namespace InvoiceManagementApp.Services
{
    public class InvoiceService
    {
        private readonly HttpClient _httpClient;

        public InvoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Reports> GetReportsAsync(DateTime? startAt = null, DateTime? endAt = null)
        {
            var response = await _httpClient.GetAsync($"api/Invoice/reports?startAt={startAt}&endAt={endAt}");
            response.EnsureSuccessStatusCode(); // Throws if not 2xx
            return await response.Content.ReadFromJsonAsync<Reports>();
        }
    }
}
