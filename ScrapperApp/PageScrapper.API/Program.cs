using Microsoft.EntityFrameworkCore;
using PageScrapper;
using PageScrapper.API.Middlewares;
using PageScrapper.Infrastructure;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
string connString = "Data Source = XXXXX\\MSSQLSERVER01; Initial Catalog= db_paesi; Integrated Security=true; TrustServerCertificate=True";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDownloaderService, DownloaderService>();
builder.Services.AddScoped<IScrapperService, ScrapperService>();
builder.Services.AddScoped<IIPAddressService, IPAddressService>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ILocalizationRepository, LocalizationRepository>();
builder.Services.AddDbContext<CountryDbContext>(opt => opt.UseSqlServer(connString));
builder.Services.AddTransient<LocalizationMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<LocalizationMiddleware>();

app.Run();
