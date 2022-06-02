using FluentValidation.Results;

namespace BusinessManagement.BusinessLayer.Extensions;

public class ValidationErrorDetails
{
    public IEnumerable<ValidationFailure> ValidationErrors { get; set; }
}
