using Chat.Domain.AggregateModels.ReciprocalContactAggregate;

namespace Chat.Domain.AggregateModels.ReciprocalContactAggregate;

/// <summary>
/// Repository interface for managing reciprocal contacts between users.
/// Handles the persistence of bidirectional user relationships.
/// </summary>
public interface IReciprocalContactRepo : IRepository<ReciprocalContact>
{
    Task<ReciprocalContact?> FindAsync(Guid userId, Guid contactUserId);
    void Add(ReciprocalContact reciprocalContact);
    void Delete(ReciprocalContact reciprocalContact);
}