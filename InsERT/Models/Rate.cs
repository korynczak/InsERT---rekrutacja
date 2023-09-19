using Newtonsoft.Json;

namespace InsERT.Models
{
    public class Rate
    {
        public int Id { get; set; }
        [JsonProperty("code")]
        public string CurrencyCode { get; set; }
        public float Mid { get; set; }
        public int ExchangeRateId { get; set; }

        public ExchangeRate ExchangeRate { get; set; }
    }
}
