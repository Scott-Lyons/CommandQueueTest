using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Shared;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var host = config["host"];

            var factory = new ConnectionFactory {HostName = host};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("SubscriberRabbitMQTest", true, false, false, null);

                    Start(channel);
                }
            }
        }

        private static void Start(IModel channel)
        {
            Console.WriteLine("Press '1' to publish the file scan");
            Console.WriteLine("Press any other key to exit");

            while (true)
            {
                var key = Console.ReadKey().Key;
                Console.WriteLine();

                if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
                {
                    var message = new FileScan
                    {
                        Id = Guid.NewGuid(),
                        FileName = "TestFile"
                    };

                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);

                    var exchange = "";
                    var routingKey = "SubscriberRabbitMQTest";

                    channel.BasicPublish(exchange, routingKey, null, body);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
