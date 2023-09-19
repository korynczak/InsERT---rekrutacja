using InsERT.Data;
using InsERT.Models;
using Microsoft.EntityFrameworkCore;

namespace InsERT
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly ExchangeRateDbContext _context;
        private readonly INbpApiClient _nbpApiClient;

        public ExchangeRateService(ExchangeRateDbContext context, INbpApiClient nbpApiClient)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _nbpApiClient = nbpApiClient ?? throw new ArgumentNullException(nameof(nbpApiClient));
        }

        public async Task<ExchangeRate> GetNowExchangeRates()
        {
            await UpdateExchangeRatesAsync();
            return await _context.ExchangeRate
                .Include(er => er.Rates)
                .FirstAsync();
        }

        public async Task<List<string>> Get5LastRate(string currencyCode)
        {
            return await _context.ExchangeRate
                .Include(er => er.Rates)
                .OrderByDescending(rate => rate.EffectiveDate)
                .Take(5)
                .Select(rate =>
                    $"{rate.EffectiveDate} - {string.Join(", ", rate.Rates.Where(r => r.CurrencyCode == currencyCode).Select(x => x.Mid))}")
                .ToListAsync();
        }

        public async Task UpdateExchangeRatesAsync()
        {
            var exchangeRatesJson = await _nbpApiClient.GetExchangeRatesAsync();
            var exchangeRateData = Newtonsoft.Json.JsonConvert.DeserializeObject<ExchangeRate[]>(exchangeRatesJson);
            foreach (var exchangeRate in exchangeRateData)
            {
                var existingExchangeRate = await _context.ExchangeRate
                    .Include(er => er.Rates)
                    .SingleOrDefaultAsync(er => er.EffectiveDate == exchangeRate.EffectiveDate);

                if (existingExchangeRate == null)
                {
                    _context.ExchangeRate.Add(exchangeRate);
                }
                else
                {
                    existingExchangeRate.Rates = exchangeRate.Rates;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}