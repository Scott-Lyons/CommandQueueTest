using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Persistence.Sql;
using Shared;

namespace Publisher
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Publisher.Example";
            var endpointConfiguration = new EndpointConfiguration("Publisher.Example");

            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.SqlVariant(SqlVariant.MsSqlServer);
            persistence.ConnectionBuilder(() => new SqlConnection(ConfigurationManager
                .ConnectionStrings["NServiceBusPublisher"].ConnectionString));

            var routing = endpointConfiguration.UseTransport<RabbitMQTransport>().Routing();
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
                var key = Console.ReadKey().Key;
                Console.WriteLine();
                
                if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
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
