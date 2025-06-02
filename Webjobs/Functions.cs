using dotnet_note_webjob_example;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Webjobs {
    public class Functions {
        private readonly PurgeClient _purgeClient;
        private readonly ILogger<PurgeClient> _logger;
        public Functions(PurgeClient purgeClient, ILogger<PurgeClient> logger) {
            _purgeClient = purgeClient;
            _logger = logger;
        }

        [Singleton]
        public async Task Purge([TimerTrigger("0 * * * * *")] TimerInfo myTimer) {
            await Purge();
        }

        public async Task Purge() {
            _logger.LogInformation($"Timer triggered at: {DateTime.Now}");
            await Task.Run(() => _purgeClient.Purge());
            _logger.LogInformation($"Database purged");
        }
    }
}
