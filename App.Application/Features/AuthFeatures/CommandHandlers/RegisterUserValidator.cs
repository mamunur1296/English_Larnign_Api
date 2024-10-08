using FluentValidation;
namespace App.Application.Features.AuthFeatures.CommandHandlers
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommend>
    {
        public RegisterUserValidator() {
            // Validate Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // Validate Password (you can customize based on your security requirements)
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            // Validate UserName
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

            // Validate Phone
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format."); // Example: checks for international format

            // Validate FirstName
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

            // Validate LastName
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.");

            //// Validate Roles (ensure the list is not empty)
            //RuleFor(x => x.Roles)
            //    .NotEmpty().WithMessage("At least one role is required.")
            //    .Must(roles => roles.All(r => !string.IsNullOrWhiteSpace(r)))
            //    .WithMessage("Roles cannot be empty or contain only whitespace.");
        }
    }
}
