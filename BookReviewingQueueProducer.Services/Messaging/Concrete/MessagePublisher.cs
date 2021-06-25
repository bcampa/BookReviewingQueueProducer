using BookReviewingQueueProducer.Services.Messaging.Contract;
using BookReviewingQueueProducer.Services.Messaging.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace BookReviewingQueueProducer.Services.Messaging.Concrete
{
    public class MessagePublisher : IMessagePublisher
    {
        public Task Publish(Message message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var queue = message.QueueName;
            channel.QueueDeclare(queue, false, false, false, null);


            var serializedMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(serializedMessage);

            channel.BasicPublish("", queue, false, null, body);

            return Task.CompletedTask;
        }
    }
}
