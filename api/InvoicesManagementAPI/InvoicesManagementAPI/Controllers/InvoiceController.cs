using InvoiceManagementBusiness.Intefaces;
using InvoiceManagementData.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("invoices", Name = "GetInvoices")]
        public async Task<IActionResult> Get([FromQuery] InvoiceFilters filters)
        {      
            try
            {
                var list = await _invoiceService.GetFiltered(filters);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpGet("reports", Name = "GetReports")]
        public async Task<IActionResult> GetReports(DateTime? startAt, DateTime? endAt)
        {
            try
            {
                var list = await _invoiceService.GetReports(startAt, endAt);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
