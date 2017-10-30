using System;
using Shared;

namespace Subscriber
{
    public static class FileScanHandler
    {
        public static void Handle(FileScan message)
        {
            Console.WriteLine($"Received file {message.Id} with name of {message.FileName}");

            //System.Threading.Thread.Sleep(5000);

            new Sender().Send(new FileDelivery { FileName = message.FileName, Id = message.Id });
        }
    }
}
