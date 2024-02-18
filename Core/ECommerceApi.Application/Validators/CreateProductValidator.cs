using ECommerceApi.Application.DTOs.Product;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Validators
{
    //Validator for Create 
    public class CreateProductValidator : AbstractValidator<Product_VM>
    {
        public CreateProductValidator()
        {
            //Validator for Name 
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(3).MaximumLength(150);

            // Validator for Stock
            RuleFor(p => p.Stock).NotNull().NotEmpty().Must(s => s >=0);
            // Validator for Price
            RuleFor(p => p.Price).NotEmpty().NotNull().Must(p => p>=0);

        }
    }
}
