using FluentValidation;


namespace App.Application.Features.EmployeeFeatures.CommandHandlers
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeValidator() {
            RuleFor(x => x.FirstName)
                  .NotEmpty().WithMessage("First name is required.")
                  .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.Address)
                .MaximumLength(250).WithMessage("Address must not exceed 250 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits.");
        }
    }
}
