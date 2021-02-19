using System;
using System.ComponentModel.DataAnnotations;

namespace AlzaTest.Services.Products.Domain.DataTransferObjects
{
    public class ProductDto
    {
        /// <summary>
        /// ID of the product, required
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the product, required
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// Uri of product image, required
        /// </summary>
        [Required]
        public string ImgUri { get; set; }
        
        /// <summary>
        /// Product price, must be greater or equal to zero, required
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Product description, optional
        /// </summary>
        public string Description { get; set; }
    }
}