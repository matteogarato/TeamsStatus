using Microsoft.Extensions.Hosting;
using TeamsStatus.ConsoleApp.Interfaces;

namespace TeamsStatus.ConsoleApp
{
    public class Worker : BackgroundService, IWorker
    {
        private readonly IHost _host;
        private readonly IStoplight _stoplight;
        private readonly ITeamStatus _teamStatus;

        public Worker(IHost host, ITeamStatus teamStatus, IStoplight stoplight)
        {
            _teamStatus = teamStatus;
            _host = host;
            _stoplight = stoplight;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await _host.StopAsync();
            }
            else
            {
                if (await _teamStatus.Available().ConfigureAwait(false))
                {
                    _stoplight.SetGreen();
                }
                else
                {
                    _stoplight.SetRed();
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartAsync(stoppingToken).ConfigureAwait(false);
        }
    }
}