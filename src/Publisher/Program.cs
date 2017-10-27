using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Persistence.Legacy;
using Shared;

namespace Publisher
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Publisher.Example";
            var endpointConfiguration = new EndpointConfiguration("Publisher.Example");
            endpointConfiguration.UsePersistence<MsmqPersistence>();
            var routing = endpointConfiguration.UseTransport<MsmqTransport>().Routing();
            routing.RouteToEndpoint(typeof(FileScan), "Subscriber.Example");

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.DisableFeature<TimeoutManager>();
            endpointConfiguration.EnableInstallers();

            var endpointInstance = Endpoint.Start(endpointConfiguration).Result;

            Start(endpointInstance).Wait();

            endpointInstance.Stop()
                .ConfigureAwait(false);
        }

        static async Task Start(IEndpointInstance endpointInstance)
        {
            Console.WriteLine("Press '1' to publish the file scan");
            Console.WriteLine("Press any other key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();
                
                if (key.Key == ConsoleKey.D1)
                {
                    var fileScan = new FileScan
                    {
                        Id = Guid.NewGuid(),
                        FileName = "File.jpg"
                    };
                    await endpointInstance.Send(fileScan)
                        .ConfigureAwait(false);
                    Console.WriteLine($"Published FileScan Event with Id {fileScan.Id}.");
                }
                else
                {
                    return;
                }
            }
        }
    }
}
