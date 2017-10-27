using System;
using System.Threading.Tasks;
using NServiceBus;
using Shared;

namespace Subscriber
{
    public class FileDeliveryHandler : IHandleMessages<FileDelivery>
    {
        public Task Handle(FileDelivery message, IMessageHandlerContext context)
        {
            Console.WriteLine($"File Delivery Command Received for file Id: {message.Id}");

            return Task.CompletedTask;
        }
    }
}
