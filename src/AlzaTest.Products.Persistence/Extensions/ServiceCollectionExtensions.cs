using AlzaTest.Products.Persistence.DBContexts;
using AlzaTest.Products.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AlzaTest.Products.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, string dbConnectionString)
            => services
                .AddDbContext<ProductsDbContext>(options => options.UseSqlServer(dbConnectionString))
                .AddRepositories();

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddScoped<IProductsRepository, ProductsRepository>();
    }
}