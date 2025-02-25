using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeamsStatus.ConsoleApp.Interfaces;

namespace TeamsStatus.ConsoleApp;

public static class Program
{
    public static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
            })
            .Build()
            .Services
            .GetService<IWorker>()
            .StartAsync(new CancellationToken(false));
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ITeamStatus, TeamsStatus>();
        services.AddSingleton<IStoplight, Stoplight>();
        services.AddSingleton<IWorker, Worker>();
    }
}