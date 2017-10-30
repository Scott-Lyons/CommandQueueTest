using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;

namespace Subscriber
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

            var factory = new ConnectionFactory { HostName = host };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("SubscriberRabbitMQTest", true, false, false, null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var fileScan = JsonConvert.DeserializeObject<FileScan>(message);

                        Console.WriteLine($"Received file {fileScan.Id} with name of {fileScan.FileName}");

                        Console.WriteLine(" [x] Received {0}", message);
                    };

                    channel.BasicConsume("SubscriberRabbitMQTest", true, consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
