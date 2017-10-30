using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Shared
{
    public class Sender
    {
        public void Send<T>(T message) where T : Message
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {

                    var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
                    var json = JsonConvert.SerializeObject(message, settings);
                    var body = Encoding.UTF8.GetBytes(json);

                    var exchange = "";
                    var routingKey = "SubscriberRabbitMQTest";

                    model.BasicPublish(exchange, routingKey, null, body);
                }
            }
        }
    }
}
