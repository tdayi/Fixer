using System.Threading;
using System.Threading.Tasks;

namespace Fixer.Core.Scheduler
{
    public interface ISchedulerTask
    {
        string TimePattern { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
