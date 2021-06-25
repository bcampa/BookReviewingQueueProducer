namespace BookReviewingQueueProducer.Services.Messaging.Messages.Book
{
    public class BookRemovedMessage : Message
    {
        public int BookId { get; set; }

        public BookRemovedMessage()
        {
            QueueName = "book-removed";
        }
    }
}
