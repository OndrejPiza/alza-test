using System.ComponentModel.DataAnnotations;
using AlzaTest.Services.Products.Domain.Model;

namespace AlzaTest.Services.Products.Domain.DataTransferObjects
{
    /// <summary>
    /// Product list response dto object
    /// </summary>
    public class ProductListDto
    {
        [Required]
        public ProductDto[] Products { get; set; }
    }
}