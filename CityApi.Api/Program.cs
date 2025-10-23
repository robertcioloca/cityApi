using CityApi.Contracts;
using CityApi.MappingProfiles;
using CityApi.Models;
using CityApi.Providers;
using CityApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var conString = builder.Configuration.GetConnectionString("CityApiDatabase") ??
    throw new InvalidOperationException("Connection string 'CityContext' not found.");
builder.Services.AddDbContext<ICityContext, CityContext>(options =>
    options.UseSqlServer(conString));

builder.Services.AddAutoMapper(typeof(CityProfile));
builder.Services.AddAutoMapper(typeof(WeatherProfile));

builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<IHttpProvider, HttpProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(o =>
    {
        o.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
