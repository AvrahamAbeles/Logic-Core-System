using Logic_Core_Server.Core.Interfaces;
using Logic_Core_Server.Data.Context;
using Logic_Core_Server.Extensions;
using Logic_Core_Server.Middleware;
using Logic_Core_Server.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Logic_Core_Server.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRateLimitingConfiguration();
builder.AddSerilogConfiguration();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
// Add services to the container.
builder.Services.AddScoped<ICalculationService, DynamicCalculationService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRateLimiter();
app.MapControllers().RequireRateLimiting("fixed");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
