using AlzaTest.Products.Domain.DataTransferObjects;
using AlzaTest.Products.Domain.Model;
using AlzaTest.Products.Persistence.DBContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlzaTest.Products.Persistence.Repositories
{
	/// <summary>
	/// Product repository to get products from data feed, eg. database
	/// </summary>
	public class ProductsRepository : IProductsRepository
	{		
		private readonly ProductsDbContext _productsDbContext;

		public ProductsRepository(ProductsDbContext productsDbContext)
		{
			_productsDbContext = productsDbContext;
		}

		/// <inheritdoc/>
		public async Task<Product> GetProductAsync(Guid productId)
		{
			return await _productsDbContext.Products
				.AsNoTracking()
				.Where(p => p.Id == productId)
				.FirstOrDefaultAsync();
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<Product>> GetProductsAsync(ushort offset, ushort limit)
		{
			return await _productsDbContext.Products
				.AsNoTracking()
				.Skip(offset)
				.Take(limit)
				.ToArrayAsync();
		}

		/// <inheritdoc/>
		public async Task<bool> UpdateProductDescriptionAsync(Guid productId, string description)
		{
			var product = await _productsDbContext.Products
				.Where(p => p.Id == productId)
				.FirstOrDefaultAsync();

			if(product == null)
			{
				return false;
			}

			product.Description = description;
			_productsDbContext.Update(product);

			var result = await _productsDbContext.SaveChangesAsync();

			return result == 1;  // One entity should be updated
		}
	}
}
