using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quotes.Application.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quotes.Persistence.Services
{
    public class DeleteOldQuotesHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public DeleteOldQuotesHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(true)
            {
                await Task.Delay(TimeSpan.FromMinutes(5));
                using var scope = _serviceProvider.CreateScope();
                var quoteService = scope.ServiceProvider.GetRequiredService<IQuoteService>();
                await quoteService.DeleteOldQuotes();
            }
        }
    }
}
