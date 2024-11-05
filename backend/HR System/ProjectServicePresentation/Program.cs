using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure;
using Application;
using Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.RegisterApplication()
    .RegisterInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandling();
var app = builder.Build();

app.UseExceptionHandler();
app.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "api";
});
app.UseOpenApi();
app.UseSwaggerGen();

app.Run();