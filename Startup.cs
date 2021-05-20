using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quotes.Application.Interfaces.Repositories;
using Quotes.Application.Interfaces.Services;
using Quotes.Persistence.Attributes;
using Quotes.Persistence.Repositories;
using Quotes.Persistence.Services;

namespace Quotes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddControllers();
            services.AddTransient<IQuoteRepository, QuoteRepository>(opt => new QuoteRepository(Configuration.GetConnectionString("Default")));
            services.AddTransient<IQuoteService, QuoteService>();
            services.AddTransient<IUserRepository, UserRepository>(opt => new UserRepository(Configuration.GetConnectionString("Default")));
            services.AddTransient<IUserService, UserService>();
            services.AddHostedService<DeleteOldQuotesHostedService>();
            services.AddScoped<AuthenticateAttribute>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
