using System;
using NServiceBus;

namespace Shared
{
    public class FileScan : ICommand
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
    }
}
