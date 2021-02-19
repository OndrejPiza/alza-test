using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AlzaTest.Services.Products.Domain.Model;
using AlzaTest.Services.Utils.Database;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace AlzaTest.Services.Products.API.Database
{
    public class DatabaseProxy : IDatabaseProxy
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger _logger;

        public DatabaseProxy(
            IDbConnection dbConnection,
            ILogger<DatabaseProxy> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }
        
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var sqlQuery = @$"SELECT p.id, p.name, p.img_uri AS imgUri, p.price, p.description FROM dbo.{TableDefinitions.ProductTableName} AS p
                            ORDER BY p.price DESC
                            OFFSET 0 ROWS FETCH NEXT 100 ROWS ONLY";

            try
            {
                return await _dbConnection.QueryAsync<Product>(sqlQuery);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during retrieving products");
            }

            return Enumerable.Empty<Product>();
        }

        public Task<Product> GetProductAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IOperationResult> UpdateProductDescriptionAsync(Guid productId, string newDescription)
        {
            var sqlQuery = @$"UPDATE dbo.{TableDefinitions.ProductTableName} SET description = '{newDescription}'
                              WHERE id = '{productId}'";

            var actionResult = new OperationResult()
            {
                IsValid = true
            };

            try
            {
                var rows = await _dbConnection.ExecuteAsync(sqlQuery);

                if (rows == 0)
                {
                    actionResult.ErrorCode = 404;
                    actionResult.IsValid = false;
                    actionResult.ErrorMessage = $"Product with ID {productId} was not found";
                }

                return actionResult;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error during product update");
                actionResult.ErrorCode = 500;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during product update");
                actionResult.ErrorCode = 500;
            }

            actionResult.IsValid = false;
            
            return actionResult;
        }
    }
}