using System;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Shared;

namespace Subscriber
{
    public class FileDeliveryHandler : IConsumeAsync<FileDelivery>
    {
        public async Task Consume(FileDelivery message)
        {
            Console.WriteLine("Message Delivered");

             await Task.CompletedTask;
        }
    }
}
