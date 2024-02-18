using MediatR;
using Microsoft.Extensions.Logging;
using My.Application.Exceptions;
using FluentValidation.Results;

namespace My.WebApi.Controllers;

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
                throw new NotFoundException("name1", "key1");
            case 400:
                _logger.LogError(result);
                ValidationResult validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("fail1", "Validation failure 1"));
                validationResult.Errors.Add(new ValidationFailure("fail2", "Validation failure 2"));
                ValidationException validationException = new ValidationException(validationResult);

                throw validationException;
            default:
                _logger.LogError(result);
                throw new BadRequestException(result);
        }

        //return await Task.FromResult(result).ConfigureAwait(true);
    }
}
