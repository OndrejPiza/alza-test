using AlzaTest.Services.Products.API.Controllers.V1;
using AlzaTest.Services.Products.API.Database;
using AlzaTest.Services.Products.Domain.Model;
using AlzaTest.Services.Utils.Database;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AlzaTest.Services.Products.UnitTests.API
{
	public class ProductControllerV1Tests
	{
		private readonly Mock<IDatabaseProxy> _dbProxyMock;
		private readonly Mock<ILogger<ProductController>> _loggerMock;
		private readonly ProductController _productController;
		private readonly Guid _productId;

		public ProductControllerV1Tests()
		{
			_productId = Guid.NewGuid();
			_dbProxyMock = new Mock<IDatabaseProxy>();			
			_loggerMock = new Mock<ILogger<ProductController>>();
			_productController = new ProductController(_dbProxyMock.Object, _loggerMock.Object);
		}

		[Fact]
		async void ProductController_GetList_DatabaseProxyProductDetailMethodIsCalled()
		{
			var productId = Guid.NewGuid();
			_dbProxyMock
				.Setup(d => d.GetProductAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Product()));

			await _productController.GetProductAsync(productId);

			_dbProxyMock.Verify(d => d.GetProductAsync(productId), Times.Once);
		}

		[Fact]
		async void ProductController_GetList_DatabaseProxyListMethodIsCalled()
		{			
			_dbProxyMock
				.Setup(d => d.GetProductsAsync()).Returns(Task.FromResult(Enumerable.Empty<Product>()));

			await _productController.GetProductsListAsync();

			_dbProxyMock.Verify(d => d.GetProductsAsync(), Times.Once);
		}

		[Fact]
		async void ProductController_GetList_DatabaseProxyUpdateMethodIsCalled()
		{
			var productId = Guid.NewGuid();
			var description = "Test description";
			_dbProxyMock
				.Setup(d => d.UpdateProductDescriptionAsync(It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.FromResult((IOperationResult)new OperationResult() { 
					IsValid = true
				}));

			await _productController.UpdateProductDescriptionAsync(productId, description);

			_dbProxyMock.Verify(d => d.UpdateProductDescriptionAsync(productId, description), Times.Once);
		}
	}
}
