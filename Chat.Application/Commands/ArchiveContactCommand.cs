using MediatR;
using System.Runtime.Serialization;

namespace Chat.ApiService.Application.Commands;

[DataContract]
public class ArchiveContactCommand(Guid userId, Guid userContactId) : IRequest<bool>
{
    [DataMember] public Guid UserId { get; private set; } = userId;

    [DataMember] public Guid UserContactId { get; private set; } = userContactId;
}