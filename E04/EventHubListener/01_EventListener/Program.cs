using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;

var ehconnectionString =
    "Endpoint=sb://ehnazewtmlns001scmshard.servicebus.windows.net/;SharedAccessKeyName=consumer-listen;SharedAccessKey=Th+6Wu4FJhtmul/IfrIOi0Tf5J9/8uFSE3B1LvBZWs0=;EntityPath=scm_om_shipper_bookings";
var ehname = "scm_om_shipper_bookings";
var blobContainerName = "consumer_group_fcr";
var blobContainerConnectionString = "DefaultEndpointsProtocol=https;AccountName=stgazewtmlns001docmndc;AccountKey=WiucguS5J3O4oRJsCIkUG90U5FvczwnoWSJbSO2bhK8cin0J45E2u9ERRcfAVXuF+mDKZFiimYEgbT2GWAlaEQ==;EndpointSuffix=core.windows.net";
var consumer_group_name = "consumer_group_fcr";

System.Console.WriteLine("Sample for eventhub listener");

var storageClient = new BlobContainerClient(blobContainerConnectionString, blobContainerName);
EventProcessorClient processor =
    new EventProcessorClient(storageClient, consumer_group_name, ehconnectionString, ehname);
processor.ProcessEventAsync += ProcessEventHandler;
processor.ProcessErrorAsync += ProcessErrorHandler;

await processor.StartProcessingAsync();
await Task.Delay(TimeSpan.FromSeconds(10));
await processor.StopProcessingAsync();

static async Task ProcessEventHandler(ProcessEventArgs args)
{
    Console.WriteLine("\t Received Event : {0}", Encoding.UTF8.GetString(args.Data.Body.ToArray()));
    await args.UpdateCheckpointAsync(args.CancellationToken);
}

static Task ProcessErrorHandler(ProcessErrorEventArgs args)
{
    return Task.CompletedTask;
}