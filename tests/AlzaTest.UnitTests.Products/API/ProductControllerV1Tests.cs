using AlzaTest.Products.API.Controllers.V1;
using AlzaTest.Products.Domain.DataTransferObjects;
using AlzaTest.Products.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AlzaTest.UnitTests.Products.API
{
	public class ProductControllerV1Tests
	{
		private readonly Mock<IProductsService> _productsServiceMock;
		private readonly Mock<ILogger<ProductsController>> _loggerMock;
		private readonly ProductsController _productController;

		public ProductControllerV1Tests()
		{
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
			_productsServiceMock
				.Setup(d => d.GetProductsAsync()).Returns(Task.FromResult(new ProductListDto()));

			await _productController.GetProductsListAsync();

			_productsServiceMock.Verify(d => d.GetProductsAsync(), Times.Once);
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
