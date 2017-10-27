using System;
using System.Threading.Tasks;
using NServiceBus;
using Shared;

namespace Subscriber
{
    public class FileScanHandler : IHandleMessages<FileScan>
    {
        public Task Handle(FileScan message, IMessageHandlerContext context)
        {
            Console.WriteLine($"File Scan Command Received for file Id: {message.Id}");
            Console.WriteLine("Sleeping for 1 second");
            System.Threading.Thread.Sleep(1000);

            return context.SendLocal(new FileDelivery
            {
                Id = message.Id,
                FileName = message.FileName
            });
        }
    }
}
