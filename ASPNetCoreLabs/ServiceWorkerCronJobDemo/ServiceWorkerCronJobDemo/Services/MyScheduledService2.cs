using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Logging;

namespace ServiceWorkerCronJobDemo.Services
{
    public class MyScheduledService2 : MyScheduledService
    {
        private readonly ILogger<MyScheduledService2> _logger;

        public MyScheduledService2(IScheduleConfig<MyScheduledService2> config, ILogger<MyScheduledService2> logger)
            : base(CronExpression.Parse(config.CronExpression), config.TimeZoneInfo, config.ExecuteOnStart)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"MyScheduledService 2 starts at {DateTime.Now:G}.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork()
        {
            _logger.LogInformation($"MyScheduledService 2 is working at time: {DateTime.Now:G}");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("MyScheduledService 2 is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
