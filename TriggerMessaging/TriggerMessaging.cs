using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace TriggerMessaging
{
  public static class TriggerMessaging
  {
    [FunctionName("negotiate")]
    public static SignalRConnectionInfo Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        [SignalRConnectionInfo(HubName = "blobEvents")] SignalRConnectionInfo connectionInfo,
        ILogger log)
    {
      log.LogInformation("Negotiating SignalR connection info.");

      return connectionInfo;
    }

  }
}
