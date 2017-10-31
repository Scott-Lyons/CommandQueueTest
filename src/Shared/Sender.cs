using System.Threading.Tasks;
using EasyNetQ;

namespace Shared
{
    public class Sender
    {
        public async Task Send<T>(T message) where T : Message
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                await bus.PublishAsync(message);
            }
        }
    }
}
