using System;
using System.IO;
using System.Threading.Tasks;
using BlobData;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BlobTriggeredFunctionApp
{
  public static class BlobTrigger
  {
    [FunctionName("BlobTrigger")]
    public static Task Run(
      [BlobTrigger("files/{name}", Connection = "AzureStorageConnectionString")] Stream myBlob, 
      string name,
      [SignalR(HubName = "blobEvents")] IAsyncCollector<SignalRMessage> signalRMessages,
      ILogger log)
    {
      log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

      var message = new BlobMessage
      {
        TriggerType = "Blob",
        FileName = name,
        TimeProcessed = DateTime.Now
      };

      log.LogInformation($"Sending Blob triggered message. Got file {name}.");

      return signalRMessages.AddAsync(
        new SignalRMessage
        {
          Target = "blobMessage",
          Arguments = new[] { message }
        });


    }
  }
}
