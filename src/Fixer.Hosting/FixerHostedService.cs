using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Fixer.Hosting
{
    public abstract class FixerHostedService : IHostedService
    {
        private Task executingTask;
        private CancellationTokenSource cancellationTokenSource;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            executingTask = ExecuteAsync(cancellationToken);

            return executingTask.IsCompleted ? executingTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (executingTask == null)
            {
                return;
            }

            cancellationTokenSource.Cancel();

            await Task.WhenAny(executingTask, Task.Delay(Timeout.Infinite, cancellationToken));

            cancellationToken.ThrowIfCancellationRequested();
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
