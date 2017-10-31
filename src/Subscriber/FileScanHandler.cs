using System;
using Shared;

namespace Subscriber
{
    public class FileScanHandler : IMessageHandler<FileScan>
    {
        public void Handle(FileScan message)
        {
            Console.WriteLine($"Received file {message.Id} with name of {message.FileName}");

            new Sender().Send(new FileDelivery {FileName = message.FileName, Id = message.Id});
        }
    }
}
