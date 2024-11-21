using FastEndpoints;
using EmployeeService.Application;
using EmployeeService.Infrastructure;
using FastEndpoints.Swagger;
using FastEndpoints.Security;
using EmployeeService.Infrastructure.Persistance;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Common;
using EmployeeService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterApplication()
                .RegisterInfrastructure(builder.Configuration);

builder.Services.SwaggerDocument();
builder.Services.AddProblemDetails();
builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["JWT:Key"]) //add this
   .AddAuthorization() //add this
   .AddFastEndpoints();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagersOnly", policy =>
        policy.RequireClaim("Role", "Manager"));
    options.AddPolicy("DevelopersOnly", policy =>
        policy.RequireClaim("Role", "Developer"));
});
builder.Services.AddHttpServiceClients(builder.Configuration);

builder.Services.AddSignalR();


var app = builder.Build();
app.MapHub<NotificationHub>("/notificationHub");
app.UseExceptionHandler();
app.UseSwaggerGen();
app.UseOpenApi();
app.UseWebSockets();
app
.UseAuthentication()
.UseAuthorization()
.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "api";
});
app.Run();