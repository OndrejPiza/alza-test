using System;
using System.Linq;
using System.Text;
using AlzaTest.Services.Products.API.Database;
using FluentMigrator;

namespace AlzaTest.Services.Products.API.Migrations
{
    [Migration(202102191342, "Initial migration of product")]
    public class InitialMigration : Migration
    {
        private const uint GeneratedProductsCount = 26;
        
        public override void Up()
        {
            Create.Table(TableDefinitions.ProductTableName)
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString().Indexed()
                .WithColumn("img_uri").AsString()
                .WithColumn("price").AsDecimal()
                .WithColumn("description").AsString().Nullable();

            var priceGenerator = new Random();
            var sb = new StringBuilder();
            sb.AppendLine($"INSERT INTO dbo.{TableDefinitions.ProductTableName} VALUES");
            
            for (int i = 1; i <= GeneratedProductsCount; i++)
            {
                var productId = Guid.NewGuid().ToString();
                var price = new decimal(priceGenerator.Next(10000, 3000000), 0, 0, false, 4);
                sb.AppendLine(
                    $"('{productId}', 'Test product {i}', 'https://cdn.alza.cz/product/{productId}.jpg', {price}, 'Test description product {i}'),");
            }
            
            var sql = sb
                .Remove(sb.Length - 2, 2)  // Remove new line characters before append semicolon
                .Append(";")
                .ToString();
            
            Execute.Sql(sql);
        }

        public override void Down()
        {
            Delete.Table(TableDefinitions.ProductTableName);
        }
    }
}