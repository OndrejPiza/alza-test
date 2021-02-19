using System;
using System.Linq;
using System.Threading.Tasks;
using AlzaTest.Services.Products.API.Database;
using AlzaTest.Services.Products.Domain.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTest.Services.Products.API.Controllers.V1
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IDatabaseProxy _databaseProxy;
        
        public ProductController(IDatabaseProxy databaseProxy)
        {
            _databaseProxy = databaseProxy;
        }
        
        [HttpGet(Name = "Get products list")]
        public async Task<ActionResult<ProductListDto>> GetProductsListAsync()
        {
            var products = await _databaseProxy.GetProductsAsync();

            return Ok(new ProductListDto()
            {
                Products = products.Select(p => p.ToDto()).ToArray()
            });
        }
        
        [HttpGet("{productId}", Name = "Get product detail")]
        public async Task<ActionResult<ProductDto>> GetProductAsync([FromRoute] Guid productId)
        {
            var product = await _databaseProxy.GetProductAsync(productId);

            if (product == null)
            {
                return NotFound($"Product with ID {productId} was not found");
            }

            return Ok(product);
        }

        [HttpPatch("{productId}", Name = "Update product description")]
        public async Task<ActionResult<Guid>> UpdateProductDescriptionAsync(
            [FromRoute] Guid productId,
            [FromBody] string newDescription
            )
        {
            var operationResult = await _databaseProxy.UpdateProductDescriptionAsync(productId, newDescription);

            if (operationResult.IsValid)
            {
                return Ok();
            }

            return StatusCode(operationResult.ErrorCode, operationResult.ErrorMessage);
        }
    }
}