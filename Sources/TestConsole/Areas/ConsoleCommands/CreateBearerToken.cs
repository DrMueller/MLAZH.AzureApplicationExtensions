using System;
using System.Threading.Tasks;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.BearerTokens.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.BearerTokens.Services;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.ConsoleExtensions.Areas.ConsoleOutput.Services;
using Newtonsoft.Json;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Areas.ConsoleCommands
{
    public class CreateBearerToken : IConsoleCommand
    {
        private readonly IBearerTokenFactory _bearerTokenFactory;
        private readonly IConsoleWriter _consoleWriter;
        public string Description { get; } = "Create Bearer Token";
        public ConsoleKey Key { get; } = ConsoleKey.D4;

        public CreateBearerToken(IBearerTokenFactory bearerTokenFactory, IConsoleWriter consoleWriter)
        {
            _bearerTokenFactory = bearerTokenFactory;
            _consoleWriter = consoleWriter;
        }

        public async Task ExecuteAsync()
        {
            const string SharePointPrincipal = "00000003-0000-0ff1-ce00-000000000000";
            const string GrantTypeClientCredentials = "client_credentials";
            const string ClientId = "5128b4e9-ccf0-4579-976f-17723fa97427";
            const string ClientSecret = "G3sA6psazP+QkNN8EciwRArGxPKYSfSW8F5UG1z/vWc=";
            const string TargetHost = "ncchdemo.sharepoint.com";
            const string TenantId = "6976be9a-b12e-4607-b5db-84b4598ccfe1";

            var request = new BearerTokenRequest(
                TenantId,
                SharePointPrincipal,
                TargetHost,
                ClientId,
                ClientSecret,
                GrantTypeClientCredentials);

            var bearerToken = await _bearerTokenFactory.CreateAsync(request);

            var serializedToken = JsonConvert.SerializeObject(bearerToken);
            _consoleWriter.WriteLine(serializedToken, ConsoleColor.Black, ConsoleColor.Cyan);
        }
    }
}