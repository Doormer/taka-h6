
using Chat.Infra;

namespace Chat.Application.Queries;

public class ChatQueries(ChatContext context)
    : IChatQueries
{
    public Task GetContactAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}