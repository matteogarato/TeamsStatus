using Microsoft.Graph;

namespace TeamsStatus.ConsoleApp.Interfaces;

public interface ITeamStatus
{
    public Task<bool> Available();

    public Task<Presence> GetAsync();
}