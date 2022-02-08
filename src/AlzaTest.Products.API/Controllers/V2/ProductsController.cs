using System;
using System.Threading.Tasks;
using AlzaTest.Products.API.Constants;
using AlzaTest.Products.Domain.DataTransferObjects;
using AlzaTest.Products.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AlzaTest.Products.API.Controllers.V2
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiVersion("2.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly ILogger _logger;

        public ProductsController(
            IProductsService productsService,
            ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _logger = logger;
        }

        [HttpGet("{productId}", Name = "Get product detail")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<ProductDto>> GetProductAsync([FromRoute] Guid productId)
        {
            _logger.LogDebug($"Recieved product detail request for product {productId}");
            var product = await _productsService.GetProductAsync(productId);

            if (product == null)
            {
                var message = $"Product with ID {productId} was not found";
                _logger.LogDebug(message);

                return NotFound(message);
            }

            _logger.LogDebug($"Returned product detail response for product {productId}");

            return Ok(product);
        }

        [HttpGet(Name = "Get products list")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductListDto), 200)]
        public async Task<ActionResult<ProductListDto>> GetProductsListAsync(
            [FromQuery] ushort limit = 10,
            [FromQuery] ushort offset = 0)
        {
            _logger.LogDebug("Recieved products list request");

            if (limit > PaginationConstants.MaximumPageSize)
            {
                _logger.LogInformation($"Client requested page size {limit}, maximum allowed {PaginationConstants.MaximumPageSize} will be used instead");

                limit = PaginationConstants.MaximumPageSize;
            }

            var productsList = await _productsService.GetProductsAsync(limit, offset);

            _logger.LogDebug("Returned products list response");

            return Ok(productsList);
        }

        [HttpPatch("{productId}", Name = "Update product description")]
        [Produces("text/plain")]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(typeof(Guid), 400)]
        public async Task<ActionResult<Guid>> UpdateProductDescriptionAsync(
            [FromRoute] Guid productId,
            [FromBody] string newDescription)
        {
            _logger.LogDebug($"Recieved product detail update request for product {productId} and description '{newDescription}'");

            if (await _productsService.UpdateProductDescriptionAsync(productId, newDescription))
            {
                _logger.LogDebug($"Returned product detail update response for product {productId} and description '{newDescription}'");

                return Ok();
            }

            _logger.LogDebug($"Error product detail update response for product {productId} and description '{newDescription}'");

            return BadRequest();
        }
    }
}
