using DynamicExpresso;
using Logic_Core_Server.Core.Interfaces;
using Logic_Core_Server.Data.Context;
using Logic_Core_Server.Extensions;
using Logic_Core_Server.Middleware;
using Logic_Core_Server.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRateLimitingConfiguration();
builder.AddSerilogConfiguration();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<ICalculationService, CalculationService>();
builder.Services.AddSingleton<Interpreter>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("TrustedFrontendOnly", policy =>
    {
         policy.WithOrigins("http://localhost:4200") 
       // policy.AllowAnyOrigin()
               .AllowAnyMethod()
              .AllowAnyHeader()              
              .AllowCredentials();               
    });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("TrustedFrontendOnly");
app.UseRateLimiter();
app.UseAuthorization();
app.MapControllers().RequireRateLimiting("fixed");

app.Run();