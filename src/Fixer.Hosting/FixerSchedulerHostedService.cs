using Fixer.Core.Scheduler;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fixer.Hosting
{
    public class FixerSchedulerHostedService : FixerHostedService
    {
        private readonly List<FixerSchedulerTaskModel> fixerSchedulerTaskModels = new List<FixerSchedulerTaskModel>();

        public FixerSchedulerHostedService(IEnumerable<ISchedulerTask> scheduledTasks)
        {
            var currentTime = DateTime.UtcNow;

            foreach (var scheduledTask in scheduledTasks)
            {
                fixerSchedulerTaskModels.Add(new FixerSchedulerTaskModel
                {
                    crontabSchedule = CrontabSchedule.Parse(scheduledTask.TimePattern),
                    schedulerTask = scheduledTask,
                    nextRunTime = currentTime
                });
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ExecuteOnceAsync(cancellationToken);

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

        private async Task ExecuteOnceAsync(CancellationToken cancellationToken)
        {
            var taskFactory = new TaskFactory(TaskScheduler.Current);
            var currentTime = DateTime.UtcNow;

            var tasksThatShouldRun = fixerSchedulerTaskModels.Where(t => t.ShouldRun(currentTime)).ToList();

            foreach (var task in tasksThatShouldRun)
            {
                task.Increment();

                await taskFactory.StartNew(async () =>
                {
                    try
                    {
                        await task.schedulerTask.ExecuteAsync(cancellationToken);
                    }
                    catch
                    {
                        throw;
                    }
                });
            }
        }
    }
}
