using MediatR;
using Microsoft.Extensions.Logging;
using My.Application.Exceptions;
using FluentValidation.Results;
using FluentValidation;

namespace My.Application.UseCases.TestError;

public class TestErrorHandler : IRequestHandler<TestErrorQuery, string>
{
    private readonly ILogger<TestErrorHandler> _logger; // Added ILogger field

    public TestErrorHandler(ILogger<TestErrorHandler> logger) // Added constructor
    {
        _logger = logger;
    }

    public async Task<string> Handle(TestErrorQuery request, CancellationToken cancellationToken)
    {
        // Query logic goes here
        var result = $"Error will be raised with ID: {request.Id}";

        switch (request.Id)
        {
            case 500:
                _logger.LogError(result);
                throw new Exception("Error 500 - Internal Server Exception");
            case 404:
                _logger.LogError(result);
                throw new NotFoundException(result);
            case 422:
                _logger.LogError(result);
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("fail1", "Validation failure 1"));
                validationResult.Errors.Add(new ValidationFailure("fail2", "Validation failure 2"));
                var validationException = new ValidationException(validationResult.Errors);

                throw validationException;

            case 400:
                _logger.LogError(result);
                throw new BadRequestException(result);

            default:
                result = $"Error will only be raised for IDs 400,404,422,500. This error #: {request.Id}";
                return await Task.FromResult(result).ConfigureAwait(true);
        }
    }
}
