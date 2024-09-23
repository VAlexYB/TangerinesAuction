using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TangerineAuction.Infrastructure;
using TangerineAuction.Infrastructure.Persistence;
using TangerineAuction.Infrastructure.Security;
using TangerinesAuction.Application;
using TangerinesAuction.Infrastructure.Settings;
using TangerinesAuction.Web;
using TangerinesAuction.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var services = builder.Services;

services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
services.AddApiAuthentication(Options.Create(jwtOptions));
services.AddControllers();

services.AddDbContext<TADbContext>(
        options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(TADbContext)));
        });

services.Configure<UniSenderSettings>(config.GetSection("UniSender"));
services.Configure<AuctionSettings>(config.GetSection("Auction"));

services
    .AddInfrastructure()
    .AddApplication()
    .AddBackgroundJobs();

services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:5500")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var app = builder.Build();

app.ApplyMigrations();

app.UseCors("AllowLocalhost");

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.None,
    Secure = CookieSecurePolicy.Always
});


app.MapControllers();


app.Run();
