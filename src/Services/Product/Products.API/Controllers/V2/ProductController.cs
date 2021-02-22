using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlzaTest.Services.Products.API.Database;
using AlzaTest.Services.Products.Domain.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AlzaTest.Services.Products.API.Controllers.V2
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiVersion("2.0")]
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
        public async Task<ActionResult<ProductListDto>> GetProductsListAsync(
            [FromQuery] int nProducts = 10,
            [FromQuery] int nOffset = 0
            )
        {
            _logger.LogDebug("Recieved products list request");

            if (nProducts > PaginationDefinitions.MaximumPageSize)
			{
                _logger.LogInformation($"Client requested page size {nProducts}, maximum allowed {PaginationDefinitions.MaximumPageSize} will be used instead");

                nProducts = PaginationDefinitions.MaximumPageSize;
            }

            var products = await _databaseProxy.GetProductsAsync(nProducts, nOffset);

            _logger.LogDebug("Returned products list response");

            return Ok(new ProductListDto()
            {
                Products = products.Select(p => p.ToDto()).ToArray()
            });
        }
    }
}
