using System;
using System.Linq;
using System.Threading.Tasks;
using AlzaTest.Services.Products.API.Database;
using AlzaTest.Services.Products.Domain.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AlzaTest.Services.Products.API.Controllers.V1
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IDatabaseProxy _databaseProxy;
        private readonly ILogger _logger;

        public ProductController(
            IDatabaseProxy databaseProxy,
            ILogger<ProductController> logger
            )
        {
            _databaseProxy = databaseProxy;
            _logger = logger;
        }
        
        [HttpGet(Name = "Get products list")]
        public async Task<ActionResult<ProductListDto>> GetProductsListAsync()
        {
            _logger.LogDebug("Recieved products list request");
            var products = await _databaseProxy.GetProductsAsync();
            _logger.LogDebug("Returned products list response");

            return Ok(new ProductListDto()
            {
                Products = products.Select(p => p.ToDto()).ToArray()
            });
        }
        
        [HttpGet("{productId}", Name = "Get product detail")]
        public async Task<ActionResult<ProductDto>> GetProductAsync([FromRoute] Guid productId)
        {
            _logger.LogDebug($"Recieved product detail request for product {productId}");
            var product = await _databaseProxy.GetProductAsync(productId);

            if (product == null)
            {
                var message = $"Product with ID {productId} was not found";
                _logger.LogDebug(message);

                return NotFound(message);
            }

            _logger.LogDebug($"Returned product detail response for product {productId}");

            return Ok(product);
        }

        [HttpPatch("{productId}", Name = "Update product description")]
        public async Task<ActionResult<Guid>> UpdateProductDescriptionAsync(
            [FromRoute] Guid productId,
            [FromBody] string newDescription
            )
        {
            _logger.LogDebug($"Recieved product detail update request for product {productId} and description '{newDescription}'");
            var operationResult = await _databaseProxy.UpdateProductDescriptionAsync(productId, newDescription);

            if (operationResult.IsValid)
            {
                _logger.LogDebug($"Returned product detail update response for product {productId} and description '{newDescription}'");

                return Ok();
            }

            _logger.LogDebug($"Error during product detail update for product {productId} and description '{newDescription}', error message {operationResult.ErrorMessage}, error code {operationResult.ErrorCode}");
            _logger.LogError($"Error during product detail update, error message {operationResult.ErrorMessage}, error code {operationResult.ErrorCode}");

            return StatusCode(operationResult.ErrorCode, operationResult.ErrorMessage);
        }
    }
}