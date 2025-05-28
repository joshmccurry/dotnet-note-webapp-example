using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace Webjobs {
    public static class Functions {
        [Singleton]
        public static void RunOnSchedule([TimerTrigger("0 * * * * *")] TimerInfo myTimer, ILogger logger)
        {
            logger.LogInformation($"Timer triggered at: {DateTime.Now}");
            // Your logic here
        }
    }
}
