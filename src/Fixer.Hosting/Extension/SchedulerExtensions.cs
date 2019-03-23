using Fixer.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;

namespace Fixer.Hosting.Extension
{
    public static class SchedulerExtensions
    {
        public static IServiceCollection AddScheduler(this IServiceCollection services, IConfiguration configuration, string sectionName = null)
        {
            if (string.IsNullOrEmpty(sectionName))
            {
                sectionName = "FixerSettings";
            }

            var section = configuration.GetSection(sectionName);
            if (section == null || !section.Exists())
            {
                throw new FormatException($"appsettings.json {sectionName} section is required!");
            }

            var fixerConfiguration = new FixerConfiguration();
            new ConfigureFromConfigurationOptions<FixerConfiguration>(section).Configure(fixerConfiguration);
            services.AddSingleton(fixerConfiguration);
            services.AddSingleton<IFixerConfiguration>(fixerConfiguration);

            return services.AddSingleton<IHostedService, FixerSchedulerHostedService>();
        }
    }
}
