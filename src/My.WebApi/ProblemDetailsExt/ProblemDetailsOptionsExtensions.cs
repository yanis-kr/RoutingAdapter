using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using My.Application.Exceptions;

namespace My.WebApi.ProblemDetailsExt;

public static class ProblemDetailsOptionsExtensions
{
    public static void MapFluentValidationException(this ProblemDetailsOptions options) =>
        options.Map<ValidationException>((ctx, ex) =>
        {
            var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();

            Dictionary<string, string[]> errors = ex.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(x => x.ErrorMessage).ToArray());

            return factory.CreateValidationProblemDetails(ctx, errors);
        });

    public static void MapMyValidationException(this ProblemDetailsOptions options) =>
    options.Map<MyValidationException>((ctx, ex) =>
    {
        var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();

        Dictionary<string, string[]> errors = ex.ValdationErrors
            //.GroupBy(x => x)
            .ToDictionary(
                x => x,
                x => new string[] { x });

        return factory.CreateValidationProblemDetails(ctx, errors);
    });
}
