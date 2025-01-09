namespace cryptoanalyzer.Models
{
    public class Cryptos
    {
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DateRetrieved { get; set; }
    }
    public class CryptoPrices:Cryptos
    {
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DateRetrieved { get; set; }
    }
    public class Cucrypto : Cryptos
    {
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DateRetrieved { get; set; }
    }
    public class FuturePrices : Cryptos
    {
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DateRetrieved { get; set; }
    }
}
   
