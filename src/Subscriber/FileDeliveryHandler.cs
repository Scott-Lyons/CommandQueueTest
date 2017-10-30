using System;
using Shared;

namespace Subscriber
{
    public class FileDeliveryHandler : IMessageHandler<FileDelivery>
    {
        public void Handle(FileDelivery message)
        {
            Console.Write("Message Delivered");
        }
    }
}
