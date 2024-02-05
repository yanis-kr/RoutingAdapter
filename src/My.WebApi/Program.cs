//using CqrsMediatrExample.Behaviors;
using Microsoft.OpenApi.Models;
using My.AppHandlers.DataStore;
using My.Domain.Models.Domain;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DomainAccount).Assembly));

builder.Services.AddSingleton<FakeDataStore>();

//builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

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
