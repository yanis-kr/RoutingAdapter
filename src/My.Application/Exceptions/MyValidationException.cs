using FluentValidation.Results;

namespace My.Application.Exceptions;

public class MyValidationException : Exception
{
    public List<string> ValdationErrors { get; set; }

    public MyValidationException(ValidationResult validationResult)
    {
        ValdationErrors = new List<string>();

        foreach (var validationError in validationResult.Errors)
        {
            ValdationErrors.Add(validationError.ErrorMessage);
        }
    }
}
