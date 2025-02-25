namespace TeamsStatus.ConsoleApp.Interfaces;

public interface IWorker
{
    public Task StartAsync(CancellationToken cancellationToken);
}