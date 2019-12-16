using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Logging;

namespace ServiceWorkerCronJobDemo.Services
{
    public class MyScheduledService1 : MyScheduledService
    {
        private readonly ILogger<MyScheduledService1> _logger;

        public MyScheduledService1(IScheduleConfig<MyScheduledService1> config, ILogger<MyScheduledService1> logger)
            : base(CronExpression.Parse(config.CronExpression), config.TimeZoneInfo, config.ExecuteOnStart)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"MyScheduledService 1 starts at {DateTime.Now:G}.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork()
        {
            _logger.LogInformation($"MyScheduledService 1 is working at time: {DateTime.Now:G}");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("MyScheduledService 1 is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
