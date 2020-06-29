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

namespace TriggerComparison
{
    public static class Messaging
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

    [FunctionName("messages")]
    public static Task SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] object message,
            [SignalR(HubName = "blobEvents")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
    {
      log.LogInformation("Chat message received.");

      return signalRMessages.AddAsync(
          new SignalRMessage
          {
            Target = "newMessage",
            Arguments = new[] { message }
          });
    }
  }
}
