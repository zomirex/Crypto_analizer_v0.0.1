namespace cryptoanalyzer.Models
{
    public class EFCryptoRepository : ICryptoRepository
    {
        private readonly StoreCryptos storeCryptos;

        public EFCryptoRepository(StoreCryptos storeCryptos)
        {
            this.storeCryptos = storeCryptos;
        }
       
       

        public List<CryptoPrices> GetAllCryptoPrices()
        {
            return storeCryptos.CryptoPrices.ToList();
        }

        public List<Cryptos> GetAllCryptos()
        {
            return storeCryptos.Cryptos.ToList();
        }

        public List<Cucrypto> GetAllCucrypto()
        {
            return storeCryptos.Cucrypto.ToList();
        }

        public List<FuturePrices> GetAllFuturePrices()
        {
            return storeCryptos.FuturePrices.ToList();
        }
    }
}
