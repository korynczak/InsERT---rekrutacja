using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsERT.Data;
using InsERT.Models;

namespace InsERT.Controllers
{
    public class ExchangeRatesController : Controller
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRatesController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService ?? throw new ArgumentNullException(nameof(exchangeRateService));
        }

        [HttpGet("rates")]
        public async Task<IActionResult> Index()
        {
            return View(await _exchangeRateService.GetNowExchangeRates()); 
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateExchangeRates()
        {
            await _exchangeRateService.UpdateExchangeRatesAsync();
            return Ok("Exchange rates updated successfully.");
        }

        [HttpGet("getLast5")]
        public  IActionResult GetLastRates(string currencyCode)
        { 
            var lastRates = new List<decimal> { 1.1m, 1.2m, 1.3m, 1.4m, 1.5m };
            var tmp = _exchangeRateService.Get5LastRate(currencyCode).Result;
            return Json(_exchangeRateService.Get5LastRate(currencyCode).Result);
        }

    }
}
