using System;

namespace Shared
{
    public class FileScan : Message
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public int SleepTime { get; set; }
    }
}
