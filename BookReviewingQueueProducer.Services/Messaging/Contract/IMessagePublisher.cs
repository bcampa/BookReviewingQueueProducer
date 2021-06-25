using BookReviewingQueueProducer.Services.Messaging.Messages;
using System.Threading.Tasks;

namespace BookReviewingQueueProducer.Services.Messaging.Contract
{
    public interface IMessagePublisher
    {
        Task Publish(Message message);
    }
}
