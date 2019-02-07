using Fixer.Core.Scheduler;
using NCrontab;
using System;

namespace Fixer.Hosting
{
    class FixerSchedulerTaskModel
    {
        public CrontabSchedule crontabSchedule { get; set; }
        public ISchedulerTask schedulerTask { get; set; }
        public DateTime lastRunTime { get; set; }
        public DateTime nextRunTime { get; set; }

        public void Increment()
        {
            lastRunTime = nextRunTime;
            nextRunTime = crontabSchedule.GetNextOccurrence(nextRunTime);
        }

        public bool ShouldRun(DateTime currentTime)
        {
            return nextRunTime < currentTime && lastRunTime != nextRunTime;
        }
    }
}
