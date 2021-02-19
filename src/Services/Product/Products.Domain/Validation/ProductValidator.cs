using AlzaTest.Services.Products.Domain.Model;
using FluentValidation;

namespace AlzaTest.Services.Products.Domain.Validation
{
    /// <summary>
    /// Product validator, validates provided <see cref="AlzaTest.Services.Products.Domain.Model.Product">Product</see>
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