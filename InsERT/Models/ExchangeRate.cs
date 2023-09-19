namespace InsERT.Models
{
    public class ExchangeRate
    {
        public int Id { get;set; }
        public DateTime EffectiveDate { get; set; }
        public List<Rate> Rates { get; set; }
    }
}
