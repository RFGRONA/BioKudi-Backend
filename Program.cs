using Biokudi_Backend.Application.Validators;
using Biokudi_Backend.Infrastructure.Config;
using Biokudi_Backend.Infrastructure.Config.Biokudi_Backend.Infrastructure.Config;
using Biokudi_Backend.Infrastructure.Data;
using Biokudi_Backend.UI.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Connection Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));

DependencyInjection.ConfigureServices(builder.Services, builder.Configuration);
LoggingConfig.ConfigureServices(builder.Services, builder.Configuration);
RateLimitConfig.ConfigureRateLimiting(builder.Services);
builder.Host.UseSerilog();
builder.Services.AddMemoryCache();
builder.Services.AddCustomCors();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<ActivityRequestDtoValidator>();
builder.Services.ConfigureResponseCompression();
builder.Services.ConfigureJsonOptions();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwagger();
builder.Services.AddOutputCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseReDoc(c =>
    {
        c.Path = "/redoc";
        c.DocumentPath = "/swagger/v1/swagger.json";
    });
}

app.UseHttpsRedirection();
app.UseExceptionHandling();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseCors("AllAllowed");
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseTokenRenewal();
app.UseAuthorization();
app.UseRateLimiter();
app.UseSanitization();
app.UseResponseCompression();
app.UseOutputCache();
app.MapControllers();
app.Run();