using System;
using EasyNetQ;

namespace Shared
{
    [Queue("FileHandlingQueue")]
    public class FileScan : Message
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
    }
}
