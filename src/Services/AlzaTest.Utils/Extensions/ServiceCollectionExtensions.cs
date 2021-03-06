using AlzaTest.Services.Utils.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AlzaTest.Services.Utils.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartupTask<T>(this IServiceCollection services)
            where T : class, IStartupTask
            => services.AddTransient<IStartupTask, T>();
    }
}