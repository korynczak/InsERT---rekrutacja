using InsERT.Models;

namespace InsERT
{
    public interface IExchangeRateService
    {
        Task<List<string>> Get5LastRate(string currencyCode);
        Task<ExchangeRate> GetNowExchangeRates();
        Task UpdateExchangeRatesAsync();
    }
}