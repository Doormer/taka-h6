using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SendMessage(SendMessageCommand command)
    {
        var messageId = await _mediator.Send(command);
        await Clients.User(command.ReceiverId.ToString())
            .SendAsync("ReceiveMessage", messageId);
    }

    public async Task MarkAsRead(MarkMessageAsReadCommand command)
    {
        await _mediator.Send(command);
        await Clients.User(command.ReaderId.ToString())
            .SendAsync("MessageRead", command.MessageId);
    }
} 