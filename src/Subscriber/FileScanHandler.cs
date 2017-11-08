using System;
using System.Threading;
using System.Threading.Tasks;
using Shared;

namespace Subscriber
{
    public class FileScanHandler : IMessageHandler<FileScan>
    {
        public async Task Handle(FileScan message)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(
                    $"Received file {message.Id} with name of {message.FileName} and slept for {message.SleepTime} milliseconds");

                Thread.Sleep(message.SleepTime);

                new Sender().Send(new FileDelivery {FileName = message.FileName, Id = message.Id});
            });
        }
    }
}
