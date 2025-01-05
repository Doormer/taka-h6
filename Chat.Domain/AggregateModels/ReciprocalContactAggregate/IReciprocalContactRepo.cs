using Chat.Domain.AggregateModels.ReciprocalContactAggregate;

namespace Chat.Domain.AggregateModels.ReciprocalContactAggregate;

public interface IReciprocalContactRepo : IRepository<ReciprocalContact>
{
    Task<ReciprocalContact?> FindAsync(Guid userId, Guid contactUserId);
    void Add(ReciprocalContact reciprocalContact);
    void Delete(ReciprocalContact reciprocalContact);
}