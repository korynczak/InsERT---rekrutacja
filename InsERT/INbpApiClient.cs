namespace InsERT
{
    public interface INbpApiClient
    {
        Task<string> GetExchangeRatesAsync();
    }
}