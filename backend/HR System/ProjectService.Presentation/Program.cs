using FastEndpoints;
using FastEndpoints.Swagger;
using Application;
using Common;
using ProjectServiceInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.RegisterApplication()
    .RegisterInfrastructure(builder.Configuration);
builder.Services.AddHttpServiceClients(builder.Configuration);
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