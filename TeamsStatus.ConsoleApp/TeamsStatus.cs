using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using TeamsStatus.ConsoleApp.Interfaces;

namespace TeamsStatus.ConsoleApp
{
    public class TeamsStatus : ITeamStatus
    {
        private static string _available = "Available";
        private readonly IConfiguration _config;
        private GraphServiceClient _graphClient;

        public TeamsStatus(IConfiguration config)
        {
            _config = config;
            // The client credentials flow requires that you request the /.default scope, and
            // preconfigure your permissions on the app registration in Azure. An administrator must
            // grant consent to those permissions beforehand. "https://graph.microsoft.com/.default"
            var scopes = new[] { _config["scopes"] };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            //common
            var tenantId = _config["tenant"];

            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var clientSecretCredential = new ClientSecretCredential(
                tenantId, _config["clientId"], _config["clientSecret"], options);

            _graphClient = new GraphServiceClient(clientSecretCredential, scopes);
        }

        public async Task<bool> Available()
        {
            var status = await GetAsync().ConfigureAwait(false);
            return status.Availability == _available;
        }

        public async Task<Presence> GetAsync()
        {
            return await _graphClient.Me.Presence
                .Request()
                .GetAsync().ConfigureAwait(false);
        }
    }
}