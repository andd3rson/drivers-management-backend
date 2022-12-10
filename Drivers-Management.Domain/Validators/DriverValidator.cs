using Drivers_Management.Domain.Models;
using FluentValidation;
namespace Drivers_Management.Domain.Validators
{
    public class DriverValidator : AbstractValidator<Driver>
    {
        public DriverValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .NotEmpty()
                .MustAsync(isValid);

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();

        }

        private async Task<bool> isValid(string arg1, CancellationToken arg2)
        {
            // TODO: create a validate method to cpf. 
            return true;
        }
    }
}