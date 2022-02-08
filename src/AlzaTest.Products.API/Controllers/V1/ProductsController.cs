using System;
using System.Threading.Tasks;
using AlzaTest.Products.API.Constants;
using AlzaTest.Products.Domain.DataTransferObjects;
using AlzaTest.Products.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace AlzaTest.Products.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
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

        /// <summary>
        /// 
        /// </summary>
        [HttpGet(Name = "Get products list")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductListDto), 200)]
        public async Task<ActionResult<ProductListDto>> GetProductsListAsync()
        {
            _logger.LogDebug("Recieved products list request");
            var productsList = await _productsService.GetProductsAsync();
            _logger.LogDebug("Returned products list response");

            return Ok(productsList);
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

                return Ok(productId);
            }

            _logger.LogDebug($"Error product detail update response for product {productId} and description '{newDescription}'");

            return BadRequest(productId);
        }
    }
}