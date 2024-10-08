using FluentValidation;

namespace App.Application.Features.RoleFeatures.CommandHandlers
{
    public class CreatrRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreatrRoleValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Role name is required.");
        }
    }
}
