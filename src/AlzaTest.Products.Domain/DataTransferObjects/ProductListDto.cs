using System.ComponentModel.DataAnnotations;
using AlzaTest.Products.Domain.Model;

namespace AlzaTest.Products.Domain.DataTransferObjects
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