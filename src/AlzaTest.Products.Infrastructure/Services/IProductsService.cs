using AlzaTest.Products.Domain.DataTransferObjects;
using System;
using System.Threading.Tasks;

namespace AlzaTest.Products.Infrastructure.Services
{
	public interface IProductsService
	{
		public const ushort DefaultOffset = 0;
		public const ushort DefaultLimit = 30;

		/// <summary>
		/// Load product from data layer with specific ID
		/// </summary>
		/// <param name="productId"></param>
		/// <returns>Awaitable task to get product or null if product with ID does not exist</returns>
		Task<ProductDto> GetProductAsync(Guid productId);

		/// <summary>
		/// Load products from data layer with default values for limit and offset
		/// </summary>
		/// <returns>Awaitable task to get products list</returns>
		Task<ProductListDto> GetProductsAsync();

		/// <summary>
		/// Load products from data layer with specific limit and offset
		/// </summary>
		/// <returns>Awaitable task to get products list</returns>
		Task<ProductListDto> GetProductsAsync(ushort offset, ushort limit);

		/// <summary>
		/// Updates products description through data layer
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="description">New description</param>
		/// <returns>Awaitable task with result of operation</returns>
		Task<bool> UpdateProductDescriptionAsync(Guid productId, string description);
	}
}
