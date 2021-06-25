using System;

namespace BookReviewingQueueProducer.Services.Messaging.Messages.Book
{
    public class BookCreatedMessage : Message
    {
        public int BookId { get; set; }
        public DateTime CreatedAt { get; set; }

        public BookCreatedMessage()
        {
            QueueName = "book-created";
        }
    }
}
