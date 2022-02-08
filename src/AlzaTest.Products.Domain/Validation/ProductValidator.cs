using AlzaTest.Products.Domain.Model;
using FluentValidation;

namespace AlzaTest.Products.Domain.Validation
{
    /// <summary>
    /// Product validator, validates provided <see cref="Product">Product</see>
    /// </summary>
    public class ProductValidator : AbstractValidator<Product> 
    {
        public ProductValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty();
            RuleFor(p => p.Name)
                .NotEmpty();
            RuleFor(p => p.ImgUri)
                .NotEmpty();
            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
        }
    }
}