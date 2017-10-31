using System;
using System.IO;
using System.Reflection;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.Configuration;
using Shared;

namespace Subscriber
{
    class Program
    {
        public static IConfigurationRoot Configuration;

        static void Main()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            using (var bus = RabbitHutch.CreateBus("host=localhost;timeout=0"))
            {
                var autoSubscriber = new AutoSubscriber(bus, "Bob")
                {
                    GenerateSubscriptionId = c => "Test1234"
                };
                autoSubscriber.SubscribeAsync(Assembly.GetCallingAssembly());
               
                Console.ReadLine();
            }
        }
    }
}
