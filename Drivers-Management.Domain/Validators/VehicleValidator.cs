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
                .MustAsync(isValid)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Year)
                .MaximumLength(4)
                .MustAsync(isAvalidYear);

        }

        private async Task<bool> isAvalidYear(string arg1, CancellationToken arg2)
        {
            return true;
        }

        private async Task<bool> isValid(string arg1, CancellationToken arg2)
        {
            return true;
        }
    }
}