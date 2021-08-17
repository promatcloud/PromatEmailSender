using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Console.Net5
{
    public class SomeService : ISomeService
    {
        private readonly ILogger<SomeService> _logger;
        private readonly IConfiguration _config;

        public SomeService(ILogger<SomeService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public void Run()
        {
            for (int i = 0; i < _config.GetValue<int>("SomeService:LoopTimes"); i++)
            {
                _logger.LogInformation("Run number {runNumber}", i);
            }
        }
    }
}