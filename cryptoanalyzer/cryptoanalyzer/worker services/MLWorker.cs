namespace cryptoanalyzer
{
    public class MLWorker : BackgroundService
    {
        private readonly ILogger<MLWorker> _logger;
        private readonly int DelayTime = 2;
        public MLWorker(ILogger<MLWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

            }
        }
    }
    
}