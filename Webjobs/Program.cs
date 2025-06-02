using dotnet_note_webjob_example;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webjobs {
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?linkid=2250384
    internal class Program {
        public static async Task WorkingMethod(string[] args) {
            // Set up DI
            var services = new ServiceCollection();
            services.AddLogging(config => {
                config.SetMinimumLevel(LogLevel.Information);
                config.AddConsole();
            });
            services.AddSingleton<PurgeClient>();

            var serviceProvider = services.BuildServiceProvider();

            // Resolve dependencies
            var purgeClient = serviceProvider.GetRequiredService<PurgeClient>();
            var logger = serviceProvider.GetRequiredService<ILogger<PurgeClient>>();

            // Create Functions instance and run Purge
            var functions = new Functions(purgeClient, logger);
            await functions.Purge();
            //Relies on the JobManager to run the function periodically

            // Dispose if needed
            if (purgeClient is IDisposable disposable) {
                disposable.Dispose();
            }
        }

        public static async Task Main(string[] args) {

            var builder = new HostBuilder()
                .ConfigureWebJobs(b => {
                    b.AddAzureStorageCoreServices();
                    b.AddTimers(); // Add support for TimerTrigger functions
                })
                .ConfigureLogging((context, b) => {
                    b.SetMinimumLevel(LogLevel.Error);
                    b.AddFilter("Function", LogLevel.Information);
                    b.AddFilter("Host", LogLevel.Debug);
                    b.AddConsole();
                });
            //builder.Services.AddSingleton<PurgeClient>(); // Register PurgeClient as a singleton
            builder.ConfigureServices(s => {
                s.AddLogging(); // Add logging services
                s.AddSingleton<PurgeClient>(); // Register PurgeClient as a singleton
            });

            var host = builder.Build();
            using (host) {
                // The following code ensures that the WebJob will be running continuously
                await host.RunAsync();
            }
        }
    }
}


