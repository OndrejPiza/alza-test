using AlzaTest.Products.API.Controllers.V2;
using AlzaTest.Products.Domain.DataTransferObjects;
using AlzaTest.Products.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AlzaTest.UnitTests.Products.API
{
	public class ProductControllerV2Tests
	{
		private readonly Mock<IProductsService> _productsServiceMock;
		private readonly Mock<ILogger<ProductsController>> _loggerMock;
		private readonly ProductsController _productController;
		private readonly Guid _productId;

		public ProductControllerV2Tests()
		{
			_productId = Guid.NewGuid();
			_productsServiceMock = new Mock<IProductsService>();
			_loggerMock = new Mock<ILogger<ProductsController>>();
			_productController = new ProductsController(_productsServiceMock.Object, _loggerMock.Object);
		}

		[Fact]
		public async void ProductController_GetList_DatabaseProxyProductDetailMethodIsCalled()
		{
			var productId = Guid.NewGuid();
			_productsServiceMock
				.Setup(d => d.GetProductAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new ProductDto()));

			await _productController.GetProductAsync(productId);

			_productsServiceMock.Verify(d => d.GetProductAsync(productId), Times.Once);
		}

		[Fact]
		public async void ProductController_GetList_DatabaseProxyListMethodIsCalled()
		{
			ushort limit = 2;
			ushort offset = 3;
			_productsServiceMock
				.Setup(d => d.GetProductsAsync(It.IsAny<ushort>(), It.IsAny<ushort>())).Returns(Task.FromResult(new ProductListDto()));

			await _productController.GetProductsListAsync(offset, limit);

			_productsServiceMock.Verify(d => d.GetProductsAsync(offset, limit), Times.Once);
		}

		[Fact]
		public async void ProductController_GetList_DatabaseProxyUpdateMethodIsCalled()
		{
			var productId = Guid.NewGuid();
			var description = "Test description";
			_productsServiceMock
				.Setup(d => d.UpdateProductDescriptionAsync(It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.FromResult(true));

			await _productController.UpdateProductDescriptionAsync(productId, description);

			_productsServiceMock.Verify(d => d.UpdateProductDescriptionAsync(productId, description), Times.Once);
		}
	}
}
