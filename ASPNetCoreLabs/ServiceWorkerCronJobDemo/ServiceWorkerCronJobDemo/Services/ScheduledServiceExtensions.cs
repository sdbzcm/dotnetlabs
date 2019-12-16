using System;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceWorkerCronJobDemo.Services
{
    public interface IScheduleConfig<T>
    {
        string CronExpression { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
        bool ExecuteOnStart { get; set; }
    }

    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
        public bool ExecuteOnStart { get; set; } = false;
    }

    public static class ScheduledServiceExtensions
    {
        public static IServiceCollection AddScheduledHostedService<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : MyScheduledService
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
            }
            var config = new ScheduleConfig<T>();
            options.Invoke(config);
            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");
            }

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
            return services;
        }
    }
}
