using Chat.Application.Queries;
using MediatR;

namespace Chat.ApiService.Apis;

public class ChatServices(
    IMediator mediator,
    IChatQueries queries,
    ILogger<ChatServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<ChatServices> Logger { get; } = logger;
    public IChatQueries Queries { get; } = queries;
}