using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests
{
    internal class CustomeWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                configurationBuilder.AddConfiguration(integrationConfig);
            });

            builder.ConfigureServices((builder, services) =>
            {
                services
                    .Remove<DbContextOptions<AppDbContext>>()
                    .AddDbContext<AppDbContext>((sp, options) =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("appDbConnectionString"),
                            builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
            });
        }
    }
}
