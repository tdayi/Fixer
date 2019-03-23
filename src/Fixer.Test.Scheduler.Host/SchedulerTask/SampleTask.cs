using Fixer.Core.Scheduler;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fixer.Test.Scheduler.Host.SchedulerTask
{
    public class SampleTask : ISchedulerTask
    {
        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                throw new Exception("test");
            });
        }
    }
}
