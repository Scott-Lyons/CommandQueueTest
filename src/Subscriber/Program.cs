using System;
using System.Configuration;
using System.Data.SqlClient;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Persistence.Sql;

namespace Subscriber
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Subscriber.Example";
            var endpointConfiguration = new EndpointConfiguration("Subscriber.Example");
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.SqlVariant(SqlVariant.MsSqlServer);
            persistence.ConnectionBuilder(() => new SqlConnection(ConfigurationManager
                .ConnectionStrings["NServiceBusSubscriber"].ConnectionString));

            endpointConfiguration.UseTransport<RabbitMQTransport>();
            endpointConfiguration.DisableFeature<TimeoutManager>();
            
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
