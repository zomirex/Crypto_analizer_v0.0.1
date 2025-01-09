using Microsoft.Data.SqlClient;

namespace cryptoanalyzer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly int DelayTime = 2;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    Console.WriteLine("".PadLeft(100, '_'));
                    _logger.LogInformation("reciving informations ...: {time}", DateTimeOffset.Now);
                    await GetCryptoData.FetchAndSaveCryptoData();
                    //Console.WriteLine("reciving informations ...: {time}", DateTimeOffset.Now);
                    _logger.LogInformation("operation ended: {time}", DateTimeOffset.Now);
                    //Console.WriteLine("operation ended: {time}", DateTimeOffset.Now);
                    Console.WriteLine("".PadLeft(100, '_'));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error*********error: {e.Message}");
                }

                try
                {

                    _logger.LogInformation("conecting to sql server ...: {time}", DateTimeOffset.Now);

                    SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder();
                    connectionBuilder.Encrypt = false;
                    connectionBuilder.InitialCatalog = "test";
                    connectionBuilder.DataSource = ".";
                    connectionBuilder.IntegratedSecurity = true;
                    Console.WriteLine(connectionBuilder.ToString());
                    //نشون دادن همه جدول ها
                    Console.WriteLine("".PadLeft(100, '_'));
                    GetCryptoData.ShowDbTable("CryptoPrices", _logger);
                    Console.WriteLine("".PadLeft(100, '_'));
                    GetCryptoData.ShowDbTable("T_Crypto", _logger);
                    Console.WriteLine("".PadLeft(100, '_'));

                    GetCryptoData.ShowDbTable("Cucrypto", _logger);
                    Console.WriteLine("".PadLeft(100, '_'));

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"error*********: {ex.Message}");
                }

                Console.WriteLine("".PadLeft(100, '_'));
                _logger.LogInformation("reciving informations ...: {time}", DateTimeOffset.Now);
                await GetCryptoData.FetchAndSaveCryptoData();
                //Console.WriteLine("reciving informations ...: {time}", DateTimeOffset.Now);
                _logger.LogInformation("operation ended: {time}", DateTimeOffset.Now);
                //Console.WriteLine("operation ended: {time}", DateTimeOffset.Now);
                Console.WriteLine("".PadLeft(100, '_'));
                await Task.Delay(TimeSpan.FromMinutes(DelayTime), stoppingToken);

                //Console.WriteLine("".PadLeft(100, '_'));
                Console.WriteLine("".PadLeft(100, '_'));
            }
        }
    }
    
}