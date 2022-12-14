using Drivers_Management.Domain.Models;
using FluentValidation;
namespace Drivers_Management.Domain.Validators
{
    public class VehicleValidator : AbstractValidator<Vehicle>
    {
        public VehicleValidator()
        {
            RuleFor(x => x.Brand)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Plate)
                .Matches("[a-zA-Z]{3}[0-9]{4}")
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Year)
                .MaximumLength(4)
                .Must(isValidYear);

        }

        // TODO: 
        private bool isValidYear(string year)
        {
            return true;
        }

      
    }
}