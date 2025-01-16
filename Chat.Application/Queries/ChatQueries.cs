using Chat.Infra;

namespace Chat.Application.Queries;

public class ChatQueries(ChatContext context)
    : IChatQueries
{
    private readonly ChatContext _context = context;

    public Task GetContactAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}