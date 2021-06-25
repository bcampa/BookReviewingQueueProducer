namespace BookReviewingQueueProducer.Services.Messaging.Messages
{
    public abstract class Message
    {
        public string QueueName { get; set; }
    }
}
