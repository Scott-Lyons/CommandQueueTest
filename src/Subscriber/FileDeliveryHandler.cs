using System;
using Shared;

namespace Subscriber
{
    public static class FileDeliveryHandler
    {
        public static void Handle(FileDelivery message)
        {
            Console.WriteLine("Message Delivered");
        }
    }
}
