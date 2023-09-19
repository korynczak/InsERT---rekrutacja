namespace InsERT
{
    public class NbpApiClient : INbpApiClient
    {
        private readonly HttpClient _httpClient;

        public NbpApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> GetExchangeRatesAsync()
        {
            var response = await _httpClient.GetStringAsync("http://api.nbp.pl/api/exchangerates/tables/a");
            return response;
        }
    }
}