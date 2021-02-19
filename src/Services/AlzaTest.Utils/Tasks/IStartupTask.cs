using System.Threading;
using System.Threading.Tasks;

namespace AlzaTest.Services.Utils.Tasks
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}