using System;
using System.Collections.Generic;
using System.Text;

namespace BookReviewingQueueProducer.Services.Requests.Book
{
    public class PublishBookRemovedMessageRequest
    {
        public int? BookId { get; set; }
    }
}
