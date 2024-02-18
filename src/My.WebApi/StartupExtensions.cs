using CorrelationId;
using CorrelationId.DependencyInjection;
using Elastic.CommonSchema.Serilog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using My.Application;
using My.Domain;
using My.Domain.Contracts;
using My.Infrastructure.EventBus;
using My.Infrastructure.FeatureFlags;
using My.Infrastructure.Legacy;
using My.Infrastructure.Modern;
using My.Infrastructure.MySysRouter;
using My.WebApi.Middleware;
using Serilog;
using Serilog.Events;

namespace My.WebApi;

public static class StartupExtensions
{
    //add services to the DI container
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .MinimumLevel.Override("CorrelationId", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.Console(new EcsTextFormatter());
        });

        builder.Services.AddDefaultCorrelationId(options =>
        {
            options.AddToLoggingScope = true;
            options.CorrelationIdGenerator = () => Guid.NewGuid().ToString();

            options.IncludeInResponse = true;
            options.RequestHeader = "X-Correlation-Id";
        });

        //mediatR registrations
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MyDomainServiceActivator).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MyApplicationServiceActivator).Assembly));

        //automapper
        builder.Services.AddAutoMapper(typeof(MyApplicationServiceActivator).Assembly);

        //all singletons until we have a real implementation
        builder.Services.AddSingleton<IEventBus, EventBusStub>();
        builder.Services.AddSingleton<ISysRouter, MySysRouterStub>();
        builder.Services.AddSingleton<IFeatureFlag, FeatureFlagsStub>();
        builder.Services.AddSingleton<IRepositoryLegacy, RepositoryLegacyStub>();
        builder.Services.AddSingleton<IRepositoryModern, RepositoryModernStub>();

        builder.Services.AddControllers();

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

        app.UseCorrelationId();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }

        app.UseHttpsRedirection();

        //app.UseRouting();

        //app.UseAuthentication();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app.UseSerilogRequestLogging();

        return app;

    }

}
