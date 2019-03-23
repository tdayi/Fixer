using System.Threading;
using System.Threading.Tasks;

namespace Fixer.Core.Scheduler
{
    public interface ISchedulerTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
