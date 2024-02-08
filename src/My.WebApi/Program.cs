//using CqrsMediatrExample.Behaviors;
using Microsoft.OpenApi.Models;
using My.AppCore;
using My.AppHandlers;
using My.Domain;
using My.Domain.Contracts;
using My.Infrastructure.EventBus;
using My.Infrastructure.MySys2;
using My.Infrastructure.Router;
using My.Infrastructure.MySys1;
using My.Infrastructure.FeatureFlags;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MyDomainServiceActivator).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MyAppCoreServiceActivator).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MyAppHandlersServiceActivator).Assembly));

builder.Services.AddAutoMapper(typeof(MyAppCoreServiceActivator).Assembly);
//singletons
builder.Services.AddSingleton<IEventBus, EventBusStub>();
builder.Services.AddSingleton<ISysRouter, MySysRouterStub>();
builder.Services.AddSingleton<IFeatureFlag, FeatureFlagsStub>();
//transients
builder.Services.AddTransient<IRepositoryMySys1, RepositoryMySys1Stub>();
builder.Services.AddTransient<IRepositoryMySys2, RepositoryMySys2Stub>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
