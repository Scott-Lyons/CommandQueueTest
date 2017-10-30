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

            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.Subscribe<FileScan>("Test", FileScanHandler.Handle);
                bus.Subscribe<FileDelivery>("Test", FileDeliveryHandler.Handle);
                
                Console.ReadLine();
            }
        }
    }
}
