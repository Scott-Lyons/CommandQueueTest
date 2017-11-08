using System;
using System.Threading.Tasks;
using Shared;

namespace Subscriber
{
    public class FileDeliveryHandler : IMessageHandler<FileDelivery>
    {
        public async Task Handle(FileDelivery message)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Message Delivered");
            });
        }
    }
}
