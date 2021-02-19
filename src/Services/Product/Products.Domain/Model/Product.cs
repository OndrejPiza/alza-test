using System;
using System.ComponentModel.DataAnnotations;
using AlzaTest.Services.Products.Domain.DataTransferObjects;

namespace AlzaTest.Services.Products.Domain.Model
{
    /// <summary>
    /// Product database model used for CRUD operation
    /// For validation purposes, use <see cref="AlzaTest.Services.Products.Domain.Validation.ProductValidator">ProductValidator</see>
    /// </summary>
    public class Product
    {
        /// <summary>
        /// ID of the product, required
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the product, required
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Uri of product image, required
        /// </summary>
        public string ImgUri { get; set; }
        
        /// <summary>
        /// Product price, must be greater or equal to zero, required
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Product description, optional
        /// </summary>
        public string Description { get; set; }

        public ProductDto ToDto()
        {
            return new ProductDto()
            {
                Description = Description,
                Id = Id,
                Name = Name,
                Price = Price,
                ImgUri = ImgUri
            };
        }
    }    
}