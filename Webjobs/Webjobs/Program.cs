using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webjobs {
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?linkid=2250384
    internal class Program {
        // Please set AzureWebJobsStorage connection strings in appsettings.json for this WebJob to run.
        public static async Task Main(string[] args) {
            var builder = new HostBuilder()
                .ConfigureWebJobs(b => {
                    b.AddAzureStorageCoreServices();
                })
                .ConfigureLogging((context, b) =>
                {
                    b.SetMinimumLevel(LogLevel.Error);
                    b.AddFilter("Function", LogLevel.Information);
                    b.AddFilter("Host", LogLevel.Debug);
                    b.AddConsole();
                });
            


            var host = builder.Build();
            using (host) {
                // The following code ensures that the WebJob will be running continuously
                await host.RunAsync();
            }
        }
    }
}


