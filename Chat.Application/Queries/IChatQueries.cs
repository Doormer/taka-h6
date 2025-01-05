
namespace Chat.Application.Queries;

public interface IChatQueries
{
    Task GetContactAsync(Guid userId);
}