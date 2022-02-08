using AlzaTest.Products.Domain.DataTransferObjects;
using AlzaTest.Products.Persistence.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlzaTest.Products.Infrastructure.Services
{
	/// <summary>
	/// Products service for all CRUD operations with products
	/// </summary>
	public class ProductsService : IProductsService
	{
		private readonly IProductsRepository _productsRepository;

		public ProductsService(IProductsRepository productsRepository)
		{
			_productsRepository = productsRepository;
		}

		/// <inheritdoc/>
		public async Task<ProductDto> GetProductAsync(Guid productId)
		{
			var product = await _productsRepository.GetProductAsync(productId);

			return product?.ToDto();
		}

		/// <inheritdoc/>
		public async Task<ProductListDto> GetProductsAsync()
		{
			return await GetProductsAsync(IProductsService.DefaultOffset, IProductsService.DefaultLimit);
		}

		/// <inheritdoc/>
		public async Task<ProductListDto> GetProductsAsync(ushort offset, ushort limit)
		{
			var products = await _productsRepository.GetProductsAsync(offset, limit);

			return new ProductListDto()
			{
				Products = products.Select(p => p.ToDto()).ToArray()
			};
		}

		/// <inheritdoc/>
		public async Task<bool> UpdateProductDescriptionAsync(Guid productId, string description)
		{
			return await _productsRepository.UpdateProductDescriptionAsync(productId, description);
		}
	}
}
