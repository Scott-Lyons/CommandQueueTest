using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;

namespace Subscriber
{
    public class QueueProcessor : EventingBasicConsumer
    {
        public QueueProcessor(IModel model) : base(model) { }
        
        public void Start()
        {
            Model.QueueDeclare("SubscriberRabbitMQTest", true, false, false, null);
            Model.QueueBind("SubscriberRabbitMQTest", "test_exchange", string.Empty);

            Received += (model, ea) =>
            {
                var body = ea.Body;
                var messageString = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<Message>(messageString,
                    new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});

                Process(message);
            };

            Model.BasicConsume("SubscriberRabbitMQTest", true, this);
        }

        private void Process(Message message)
        {
            switch (message)
            {
                case FileScan fs:
                    new FileScanHandler().Handle(fs);
                    break;
                case FileDelivery fd:
                    new FileDeliveryHandler().Handle(fd);
                    break;
            }
        }
    }
}
