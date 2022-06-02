using BusinessManagement.Entities.DTOs;
using FluentValidation;

namespace BusinessManagement.BusinessLayer.ValidationRules.FluentValidation;

public class AuthorizationDtoLoginWithPhoneValidator : AbstractValidator<AuthorizationDto>
{
    public AuthorizationDtoLoginWithPhoneValidator()
    {
        RuleFor(a => a.Phone).NotEmpty();
        RuleFor(a => a.Password).NotEmpty();
    }
}
