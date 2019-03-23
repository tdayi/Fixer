using Fixer.Hosting;
using Fixer.Hosting.Extension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Fixer.Test
{
    public class FixerTest
    {
        [Fact]
        public void AddSchedulerTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScheduler(null);

            var serviceProvider = services.BuildServiceProvider();
            var fixerSchedulerHostedService = serviceProvider.GetService<IHostedService>();

            Assert.Equal(typeof(FixerSchedulerHostedService), fixerSchedulerHostedService.GetType());
        }
    }
}
