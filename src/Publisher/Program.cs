using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Shared;

namespace Publisher
{
    class Program
    {
        private static IConfigurationRoot _config;

        static void Main(string[] args)
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Start();
        }

        private static void Start()
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
                        JourneyId = Guid.NewGuid(),
                        Id = Guid.NewGuid(),
                        FileName = "TestFile"
                    };

                    new Sender().Send(message).ConfigureAwait(false);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
