using AlzaTest.Products.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AlzaTest.Products.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// register necessary services from infrastructure layer
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConnectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
            => services
                .AddScoped<IProductsService, ProductsService>();
    }
}