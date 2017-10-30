using EasyNetQ;

namespace Shared
{
    public class Sender
    {
        public void Send<T>(T message) where T : Message
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.Publish(message);
            }
        }
    }
}
