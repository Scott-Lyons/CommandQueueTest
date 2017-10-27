
using System;
using NServiceBus;

namespace Shared
{
    public class FileDelivery : ICommand
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
    }
}
