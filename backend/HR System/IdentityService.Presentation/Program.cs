using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure;
using Core;
using FastEndpoints.Security;
using Application;
using Common;
using FastEndpoints.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.RegisterApplication()
                .RegisterInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandling();
builder.Services.AddHttpServiceClients(builder.Configuration);
builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["JWT:Key"])
    .AddAuthorization() //add this
    .AddFastEndpoints();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagersOnly", policy =>
        policy.RequireClaim("Role", "Manager"));
});

var app = builder.Build();

app.UseExceptionHandler();
app.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "api";
});
app.UseSwaggerGen();

app.Run();