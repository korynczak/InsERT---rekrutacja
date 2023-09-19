using InsERT;
using InsERT.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddRazorPages();
builder.Services.AddScoped<INbpApiClient, NbpApiClient>();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddDbContext<ExchangeRateDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("ExchangeRateConnection")));
var app = builder.Build();

app.MapControllers();

app.Run();
