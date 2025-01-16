using Chat.Application.Commands.SendMessage;
using Chat.Application.Commands.MarkMessageAsRead;
using Chat.Application.Queries.GetMessages;
using Chat.Application.Queries.GetUnreadMessages;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> SendMessage([FromBody] SendMessageCommand command)
        {
            var messageId = await _mediator.Send(command);
            return Ok(messageId);
        }

        [HttpGet("{user1Id}/{user2Id}")]
        public async Task<ActionResult<List<MessageDto>>> GetMessages(
            Guid user1Id, 
            Guid user2Id,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 20)
        {
            var query = new GetMessagesQuery
            {
                User1Id = user1Id,
                User2Id = user2Id,
                Skip = skip,
                Take = take
            };
            
            var messages = await _mediator.Send(query);
            return Ok(messages);
        }

        [HttpGet("unread/{userId}")]
        public async Task<ActionResult<List<MessageDto>>> GetUnreadMessages(Guid userId)
        {
            var query = new GetUnreadMessagesQuery { UserId = userId };
            var messages = await _mediator.Send(query);
            return Ok(messages);
        }

        [HttpPost("{messageId}/read")]
        public async Task<ActionResult> MarkAsRead(Guid messageId)
        {
            var command = new MarkMessageAsReadCommand
            {
                MessageId = messageId,
                ReaderId = GetCurrentUserId() // 需要实现获取当前用户ID的方法
            };
            
            var result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }

        private Guid GetCurrentUserId()
        {
            // 从 JWT Token 或其他认证机制获取当前用户ID
            // 这里需要根据你的认证方案来实现
            throw new NotImplementedException();
        }
    }
} 