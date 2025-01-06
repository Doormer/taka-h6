using Chat.Domain.AggregateModels.ReciprocalContactAggregate;
using Chat.Domain.SeedWork;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infra.Repositories;

public class ReciprocalContactRepo(ChatContext context) : IReciprocalContactRepo
{
    private readonly ChatContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public Task<ReciprocalContact?> FindAsync(Guid userId, Guid contactUserId)
    {   
        throw new NotImplementedException();
    }

    public void Add(ReciprocalContact reciprocalContact)
    {
        var contacts = reciprocalContact.GetReciprocalContact();
        _context.contacts.AddRange(contacts.Item1, contacts.Item2);
    }

    public void Delete(ReciprocalContact reciprocalContact)
    {
        throw new NotImplementedException();
    }

    public async Task<ReciprocalContact?> FindByUserIdAndContactIdAsync(Guid userId, Guid contactId)
    {
        return await _context.ReciprocalContacts
            .FirstOrDefaultAsync(c => 
                (c.UserId == userId && c.UserContactId == contactId) ||
                (c.UserId == contactId && c.UserContactId == userId));
    }
}