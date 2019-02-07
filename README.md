# .Net Core Lightweight Scheduler Task Library

Fixer contains the .Net Core Api support for creating custom tasks with IHostedService interface and uses the NCrontab for the scheduler time pattern.

# How to use?

Add the Fixer latest version nuget package to your .Net Core Api project.

Nuget Address: https://www.nuget.org/packages/Fixer/

**Install-Package Fixer -Version 1.0.1**

## Create Task Class

create your task class by implementing ISchedulerTask interface

    using Fixer.Core.Scheduler;
    using System.Threading;
    using System.Threading.Tasks;
    
    namespace FixerSample.Tasks
    {
        public class FixerSampleTask : ISchedulerTask
        {
            public string TimePattern => "* * * * *";
    
            public Task ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.Run(() =>
                {
                    // your code...
                    
                }, cancellationToken);
            }
        }
    }


## Dependency Injection

Add your scheduler and task class to your .Net Core Api project with Dependency Injection

    services.AddSingleton<ISchedulerTask, FixerSampleTask>();
    services.AddScheduler();

## NCrontab (Crontab for .NET)

Fixer, uses the open source NCrontab library for scheduler task time pattern

http://www.raboof.com/projects/ncrontab/
