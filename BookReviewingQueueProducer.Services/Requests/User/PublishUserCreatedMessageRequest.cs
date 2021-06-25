using System;

namespace BookReviewingQueueProducer.Services.Requests.User
{
    public class PublishUserCreatedMessageRequest
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
