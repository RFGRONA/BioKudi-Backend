using Biokudi_Backend.Infrastructure.Config;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Connection Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));

DependencyInjection.ConfigureServices(builder.Services, builder.Configuration);
LoggingConfig.ConfigureServices(builder.Services, builder.Configuration);
builder.Host.UseSerilog();
builder.Services.AddMemoryCache();
builder.Services.AddCustomCors();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllAllowed");
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
