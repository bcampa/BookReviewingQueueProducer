using BookReviewingQueueProducer.Services.Messaging.Contract;
using BookReviewingQueueProducer.Services.Messaging.Messages.Book;
using BookReviewingQueueProducer.Services.Messaging.Messages.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult PublishBookCreatedMessage(BookCreatedMessage message)
        {
            message ??= new BookCreatedMessage();

            if (message.BookId <= 0)
            {
                var random = new Random();
                message.BookId = random.Next(100);
            }

            message.CreatedAt = DateTime.Now;

            _messagePublisher.Publish(message);

            return Ok(message);
        }

        [HttpPost("PublishBookRemovedMessage")]
        [ProducesResponseType(typeof(BookRemovedMessage), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PublishBookRemovedMessage(BookRemovedMessage message)
        {
            if (message == null || message.BookId <= 0)
            {
                return BadRequest("Please submit a valid Id");
            }

            _messagePublisher.Publish(message);

            return Ok(message);
        }

        [HttpPost("PublishUserCreatedMessage")]
        [ProducesResponseType(typeof(UserCreatedMessage), StatusCodes.Status200OK)]
        public IActionResult PublishUserCreatedMessage(UserCreatedMessage message)
        {
            message ??= new UserCreatedMessage();

            if (message.UserId == Guid.Empty)
                message.UserId = Guid.NewGuid();

            if (string.IsNullOrEmpty(message.Name))
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

            _messagePublisher.Publish(message);

            return Ok(message);
        }
    }
}
