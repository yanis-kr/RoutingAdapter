using CorrelationId;
using CorrelationId.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using My.Application;
using My.Application.Exceptions;
using My.Domain;
using My.Domain.ConfigOptions;
using My.Domain.Contracts;
using My.Infrastructure.EventBus;
using My.Infrastructure.FeatureFlags;
using My.Infrastructure.Legacy;
using My.Infrastructure.Modern;
using My.Infrastructure.MySysRouter;
using My.WebApi.HttpHandlers;
using My.WebApi.Middleware;
using My.WebApi.ProblemDetailsExt;
using Serilog;
using Serilog.Events;

namespace My.WebApi;

public static class StartupExtensions
{
    //add services to the DI container
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration
                .WriteTo.Console(formatProvider: null)
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.WithEnvironmentName()
                .MinimumLevel.Override("CorrelationId", LogEventLevel.Error)
                .Enrich.FromLogContext();
        });

        builder.Services.AddDefaultCorrelationId(options =>
        {
            options.AddToLoggingScope = true;
            options.CorrelationIdGenerator = () => Guid.NewGuid().ToString();
            options.IncludeInResponse = true;
            options.RequestHeader = HeaderConstants.HeaderCorrelationId;
        });

        //mediatR registrations
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MyDomainServiceActivator).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MyApplicationServiceActivator).Assembly));

        //automapper
        builder.Services.AddAutoMapper(typeof(MyApplicationServiceActivator).Assembly);

        //all singletons until we have a real implementation
        builder.Services.AddSingleton<IEventBus, EventBusStub>();
        //builder.Services.AddSingleton<ISysRouter, MySysRouterStub>();
        builder.Services.AddSingleton<IFeatureFlag, FeatureFlagsStub>();
        builder.Services.AddSingleton<IRepositoryLegacy, RepositoryLegacyStub>();
        builder.Services.AddSingleton<IRepositoryModern, RepositoryModernStub>();

        //PropagateHeaderHandler will use IHttpContextAccessor to access the current request and retrieve
        //the header value to add it to the outgoing request. 
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<PropagateHeaderHandler>();

        //add typed http client with custom handler
        builder.Services.AddHttpClient<ISysRouter, MySysRouterHttpStub>()
            .AddHttpMessageHandler<PropagateHeaderHandler>();

        builder.Services.Configure<CustomLogging>(builder.Configuration.GetSection("CustomLogging"));

        builder.Services.AddControllers();

        #region Behavior Options
        //builder.Services.Configure<ApiBehaviorOptions>(options =>
        //{
        //    // Redefine the factory method that is used to create a 400 Bad Request response when Model validation fails.
        //    // In this example, the status code is replaced using 422 instead of 400.
        //    options.InvalidModelStateResponseFactory = actionContext =>
        //    {
        //        var errors = actionContext.ModelState.Where(e => e.Value?.Errors.Any() ?? false)
        //            .SelectMany(e => e.Value.Errors.Select(x => new ValidationError(e.Key, x.ErrorMessage)));

        //        var httpContext = actionContext.HttpContext;
        //        var statusCode = StatusCodes.Status422UnprocessableEntity;
        //        var problemDetails = new ProblemDetails
        //        {
        //            Status = statusCode,
        //            Type = $"https://httpstatuses.com/{statusCode}",
        //            Instance = httpContext.Request.Path,
        //            Title = "Validation errors occurred"
        //        };

        //        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
        //        problemDetails.Extensions.Add("errors", errors);

        //        var result = new ObjectResult(problemDetails)
        //        {
        //            StatusCode = statusCode
        //        };

        //        return result;
        //    };
        //});
        #endregion

        builder.Services.AddProblemDetails(options =>
        {
            // Custom mapping function for FluentValidation's ValidationException.
            options.MapFluentValidationException();
            options.MapMyValidationException();

            options.Map<ApplicationException>(ex => new StatusCodeProblemDetails(StatusCodes.Status400BadRequest));
            options.Map<NotFoundException>(ex => new ProblemDetails
            {
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = ex.Message
            });

            options.MapToStatusCode<BadRequestException>(StatusCodes.Status400BadRequest);

            // You can configure the middleware to re-throw certain types of exceptions, all exceptions or based on a predicate.
            // This is useful if you have upstream middleware that needs to do additional handling of exceptions.
            options.Rethrow<NotSupportedException>();

            // This will map NotImplementedException to the 501 Not Implemented status code.
            options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

            // This will map HttpRequestException to the 503 Service Unavailable status code.
            //options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);

            // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
            // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);

        });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });

        builder.Services.AddHttpContextAccessor();

        return builder.Build();
    }

    //configure the HTTP request/response pipeline
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseProblemDetails();

        app.UseCorrelationId();

        if (app.Environment.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }

        app.UseHttpsRedirection();

        //app.UseRouting();

        //app.UseAuthentication();
        //app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseMiddleware<RequestResponseLoggingMiddleware>();
        //app.UseMiddleware<EchoMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app.UseSerilogRequestLogging();

        return app;

    }
    internal record ValidationError(string Name, string Message);
}
