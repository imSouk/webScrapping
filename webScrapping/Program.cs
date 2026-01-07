using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using webScrapping.Services;
using webScrapping.Interfaces;
using webScrapping.Repositories;
using webScrapping.context;

namespace webScrapping;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration["ConnectionStrings:DefaultConnection"];
                
                services.AddDbContext<context.StorageBroker>(options =>
                    options.UseSqlServer(connectionString));
                
                services.AddHttpClient<IMatchScraper, MatchScraperService>();
                
                services.AddScoped<IMatchRepository, MatchRepository>();
                services.AddScoped<MatchOrchestratorService>();
            })
            .Build();

        var orchestrator = host.Services.GetRequiredService<MatchOrchestratorService>();
        
        await orchestrator.RunScrapingAsync();
        await Task.Delay(5000);
    }
}
