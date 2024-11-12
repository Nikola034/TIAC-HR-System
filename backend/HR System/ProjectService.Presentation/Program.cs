using FastEndpoints;
using FastEndpoints.Swagger;
using Application;
using Common;
using FastEndpoints.Security;
using ProjectServiceInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.RegisterApplication()
    .RegisterInfrastructure(builder.Configuration);
builder.Services.AddHttpServiceClients(builder.Configuration);
builder.Services.AddExceptionHandling();
builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["JWT:Key"])
    .AddAuthorization() //add this
    .AddFastEndpoints();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagersOnly", policy =>
        policy.RequireClaim("Role", "Manager"));
    options.AddPolicy("DevelopersOnly", policy =>
        policy.RequireClaim("Role", "Developer"));
});

var app = builder.Build();

app.UseExceptionHandler();
app.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "api";
});
app.UseOpenApi();
app.UseSwaggerGen();
app.UseAuthentication()
    .UseAuthorization();
app.Run();