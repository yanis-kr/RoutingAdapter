using Microsoft.OpenApi.Models;
using My.AppCore;
using My.AppHandlers;
using My.Domain;
using My.Domain.Contracts;
using My.Infrastructure.EventBus;
using My.Infrastructure.MySys2;
using My.Infrastructure.MySys1;
using My.Infrastructure.FeatureFlags;
using My.Infrastructure.MySysRouter;
using My.WebApi.Middleware;

namespace My.WebApi;

public static class StartupExtensions
{
    //add services to the DI container
    public static WebApplication ConfigureServices(
    this WebApplicationBuilder builder)
    {
        //mediatR registrations
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MyDomainServiceActivator).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MyAppMappingsServiceActivator).Assembly));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MyAppServicesServiceActivator).Assembly));

        //automapper
        builder.Services.AddAutoMapper(typeof(MyAppMappingsServiceActivator).Assembly);

        //all singletons until we have a real implementation
        builder.Services.AddSingleton<IEventBus, EventBusStub>();
        builder.Services.AddSingleton<ISysRouter, MySysRouterStub>();
        builder.Services.AddSingleton<IFeatureFlag, FeatureFlagsStub>();
        builder.Services.AddSingleton<IRepositoryMySys1, RepositoryMySys1Stub>();
        builder.Services.AddSingleton<IRepositoryMySys2, RepositoryMySys2Stub>();

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

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }

        app.UseHttpsRedirection();

        //app.UseRouting();

        //app.UseAuthentication();

        app.UseCustomExceptionHandler();

        app.UseAuthorization();

        app.MapControllers();

        return app;

    }

}
