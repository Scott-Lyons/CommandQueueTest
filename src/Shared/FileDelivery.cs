using System;

namespace Shared
{
    public class FileDelivery : Message
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
    }
}
