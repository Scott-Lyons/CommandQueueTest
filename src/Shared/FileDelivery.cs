using System;
using EasyNetQ;

namespace Shared
{
    [Queue("FileHandlingQueue")]
    public class FileDelivery : Message
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
    }
}
