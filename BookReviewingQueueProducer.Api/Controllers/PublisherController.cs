using BookReviewingQueueProducer.Services.Messaging.Contract;
using BookReviewingQueueProducer.Services.Messaging.Messages.Book;
using BookReviewingQueueProducer.Services.Messaging.Messages.User;
using BookReviewingQueueProducer.Services.Requests.Book;
using BookReviewingQueueProducer.Services.Requests.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookReviewingQueueProducer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IMessagePublisher _messagePublisher;

        public PublisherController(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        [HttpPost("PublishBookCreatedMessage")]
        [ProducesResponseType(typeof(BookCreatedMessage), StatusCodes.Status200OK)]
        public IActionResult PublishBookCreatedMessage(PublishBookCreatedMessageRequest request)
        {
            var message = new BookCreatedMessage
            {
                CreatedAt = DateTime.Now
            };

            if (request != null && request.BookId.HasValue && request.BookId.Value > 0)
                message.BookId = request.BookId.Value;
            else
            {
                var random = new Random();
                message.BookId = random.Next(1, 101);
            }

            _messagePublisher.Publish(message);

            return Ok(message);
        }

        [HttpPost("PublishBookRemovedMessage")]
        [ProducesResponseType(typeof(BookRemovedMessage), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PublishBookRemovedMessage(PublishBookRemovedMessageRequest request)
        {
            if (request == null || !request.BookId.HasValue || request.BookId <= 0)
            {
                return BadRequest("Please submit a valid Id");
            }

            var message = new BookRemovedMessage
            {
                BookId = request.BookId.Value
            };

            _messagePublisher.Publish(message);

            return Ok(message);
        }

        [HttpPost("PublishUserCreatedMessage")]
        [ProducesResponseType(typeof(UserCreatedMessage), StatusCodes.Status200OK)]
        public IActionResult PublishUserCreatedMessage(PublishUserCreatedMessageRequest request)
        {
            var message = new UserCreatedMessage();

            if (request.UserId != Guid.Empty)
                message.UserId = request.UserId;
            else
                message.UserId = Guid.NewGuid();

            if (string.IsNullOrEmpty(request.Name))
            {
                var possibleNames = new string[]
                {
                    "Rebecca",
                    "Bruno",
                    "Gabi",
                    "Pedro",
                    "Vitória",
                    "Fábio",
                    "José",
                    "Maria",
                    "Joana"
                };
                var random = new Random();
                message.Name = possibleNames[random.Next(possibleNames.Length)];
            }
            else
                message.Name = request.Name;

            _messagePublisher.Publish(message);

            return Ok(message);
        }
    }
}
