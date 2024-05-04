using CompanyEmployees;
using CompanyEmployees.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers()
	.AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);

var app = builder.Build();

app.UseExceptionHandler(opt => { });

if (app.Environment.IsProduction())
	app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
	ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
