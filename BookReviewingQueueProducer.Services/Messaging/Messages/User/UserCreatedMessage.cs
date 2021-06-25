using System;

namespace BookReviewingQueueProducer.Services.Messaging.Messages.User
{
    public class UserCreatedMessage : Message
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public UserCreatedMessage()
        {
            QueueName = "user-created";
        }
    }
}
