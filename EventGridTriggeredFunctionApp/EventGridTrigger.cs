// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.Threading.Tasks;
using System.IO;
using BlobData;

namespace EventGridTriggerFunctionApp
{
  public static class EventGridTrigger
  {
    [FunctionName("EventGridTrigger")]
    public static Task Run(
      [EventGridTrigger] EventGridEvent eventGridEvent,
      [SignalR(HubName = "blobEvents")] IAsyncCollector<SignalRMessage> signalRMessages,
      ILogger log)
    {
      log.LogInformation(eventGridEvent.Data.ToString());

      var blobEvent = JsonConvert.DeserializeObject<BlobEventData>(eventGridEvent.Data.ToString());

      if (blobEvent == null)
      {
        log.LogError("Could not parse blob event data from EventGrid event.");
        throw new Exception("Could not parse blob event data from EVentGrid event.");
      }

      var fileUri = new Uri(blobEvent.Url);
      string fileName;

      fileName = Path.GetFileName(fileUri.AbsolutePath);

      var message = new BlobMessage
      {
        TriggerType = "Event Grid",
        FileName = fileName,
        TimeProcessed = DateTime.Now
      };

      log.LogInformation($"Sending Event Grid triggered message. Got file {fileName}.");

      return signalRMessages.AddAsync(
        new SignalRMessage
        {
          Target = "blobMessage",
          Arguments = new[] { message }
        });

    }
  }
}
