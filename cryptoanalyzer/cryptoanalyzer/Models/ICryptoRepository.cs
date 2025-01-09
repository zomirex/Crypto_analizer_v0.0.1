namespace cryptoanalyzer.Models
{
    public interface ICryptoRepository
    {
        public List<CryptoPrices> GetAllCryptoPrices();
        public List<Cryptos> GetAllCryptos();
        public List<Cucrypto> GetAllCucrypto();
        public List<FuturePrices> GetAllFuturePrices();

    }
}
