using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quotes.Migrations;
using Quotes.Seeds;
using System.Threading.Tasks;

namespace Quotes
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            string connectionString;
            using (var scope = host.Services.CreateScope())
            {
                IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                connectionString = configuration.GetConnectionString("Default");
            }
            Migration.ConnectionString = connectionString;
            await Migration.MigrateAsync();
            DefaultUsers.ConnectionString = connectionString;
            await DefaultUsers.AddDefaultUsers();
            DefaultAuthors.ConnectionString = connectionString;
            await DefaultAuthors.AddDefaultAuthors();
            DefaultCategories.ConnectionString = connectionString;
            await DefaultCategories.AddDefaultCategories();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
