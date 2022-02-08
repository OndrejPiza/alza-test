using AlzaTest.Products.Domain.DataTransferObjects;
using AlzaTest.Products.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlzaTest.Products.Persistence.Repositories
{
	public interface IProductsRepository
	{
		/// <summary>
		/// Get products with specific offset and limit
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="limit"></param>
		/// <returns>Awaitable task with products collection</returns>
		public Task<IEnumerable<Product>> GetProductsAsync(ushort offset, ushort limit);

		/// <summary>
		/// Get product with specific ID or null in case product does not exist
		/// </summary>
		/// <param name="productId"></param>
		/// <returns>Awaitable task with product</returns>
		public Task<Product> GetProductAsync(Guid productId);

		/// <summary>
		/// Updates products description
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="description">New description</param>
		/// <returns>Awaitable task with result of operation</returns>
		public Task<bool> UpdateProductDescriptionAsync(Guid productId, string description);
	}
}
