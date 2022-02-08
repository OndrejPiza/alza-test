using System.Threading;
using System.Threading.Tasks;

namespace AlzaTest.Utils.Tasks
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}