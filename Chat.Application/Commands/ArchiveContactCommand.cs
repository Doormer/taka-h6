using MediatR;
using System.Runtime.Serialization;

namespace Chat.ApiService.Application.Commands;

/// <summary>
/// Command to archive or unarchive a contact.
/// Represents a request to toggle the archive status of a specific contact.
/// </summary>
[DataContract]
public class ArchiveContactCommand(Guid userId, Guid userContactId) : IRequest<bool>
{
    [DataMember] public Guid UserId { get; private set; } = userId;

    [DataMember] public Guid UserContactId { get; private set; } = userContactId;
}