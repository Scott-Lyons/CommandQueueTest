using System;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Shared;

namespace Subscriber
{
    public class FileScanHandler : IConsumeAsync<FileScan>
    {
        public async Task Consume(FileScan message)
        {
            Console.WriteLine($"Received file {message.Id} with name of {message.FileName}");

            await new Sender().Send(new FileDelivery { FileName = message.FileName, Id = message.Id });
        }
    }
}
