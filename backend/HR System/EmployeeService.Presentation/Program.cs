using FastEndpoints;
using EmployeeService.Application;
using EmployeeService.Infrastructure;
using FastEndpoints.Swagger;
using FastEndpoints.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterApplication()
                .RegisterInfrastructure(builder.Configuration);

builder.Services.SwaggerDocument();
builder.Services.AddProblemDetails();
builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["JWT:Key"]) //add this
   .AddAuthorization() //add this
   .AddFastEndpoints();


var app = builder.Build();
app.UseExceptionHandler();
app.UseSwaggerGen();
app.UseOpenApi();

app
.UseAuthentication()
.UseAuthorization()
.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "api";
});
app.Run();