using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing.Impl;

namespace Subscriber
{
    class Program
    {
        public static IConfigurationRoot Configuration;

        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var host = Configuration["host"];

            var factory = new ConnectionFactory { HostName = host };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var queueProcessor = new QueueProcessor(channel);
                    queueProcessor.Start();

                    Console.WriteLine("Press any key to exit");
                    Console.ReadLine();
                }
            }
        }
    }
}
