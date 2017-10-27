using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Persistence.Legacy;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Subscriber.Example";
            var endpointConfiguration = new EndpointConfiguration("Subscriber.Example");
            endpointConfiguration.UsePersistence<MsmqPersistence>();
            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.DisableFeature<TimeoutManager>();
            var routing = transport.Routing();

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = Endpoint.Start(endpointConfiguration).Result;
                
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
