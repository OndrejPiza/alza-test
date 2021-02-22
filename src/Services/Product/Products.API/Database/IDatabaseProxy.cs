using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlzaTest.Services.Products.Domain.Model;
using AlzaTest.Services.Utils.Database;

namespace AlzaTest.Services.Products.API.Database
{
    public interface IDatabaseProxy
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<IEnumerable<Product>> GetProductsAsync(int numberOfProducts, int pageOffset);

        Task<Product> GetProductAsync(Guid productId);

        Task<IOperationResult> UpdateProductDescriptionAsync(Guid productId, string description);
    }
}