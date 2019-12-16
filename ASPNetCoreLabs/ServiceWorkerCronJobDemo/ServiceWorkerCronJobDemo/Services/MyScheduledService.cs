using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;

namespace ServiceWorkerCronJobDemo.Services
{
    public abstract class MyScheduledService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;
        private readonly bool _executeOnStart;

        protected MyScheduledService(CronExpression expression, TimeZoneInfo timeZoneInfo, bool executeOnStart)
        {
            _expression = expression;
            _timeZoneInfo = timeZoneInfo;
            _executeOnStart = executeOnStart;
        }

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            var occurrences = _expression.GetOccurrences(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(2), _timeZoneInfo).ToArray();
            if (occurrences.Any())
            {
                var period = occurrences.Length == 1 ? TimeSpan.FromMilliseconds(-1) : occurrences[1] - occurrences[0];
                var dueTime = _executeOnStart ? TimeSpan.Zero : occurrences[0] - DateTimeOffset.Now;
                _timer = new Timer(async s => await DoWork(), null, dueTime, period);
            }
            return Task.CompletedTask;
        }

        public virtual Task DoWork()
        {
            return Task.CompletedTask;
        }

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
