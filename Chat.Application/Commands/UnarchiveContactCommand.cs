using MediatR;
using System.Runtime.Serialization;
namespace Chat.Application.Commands;

[DataContract]

public class UnarchiveContactCommand(Guid userId, Guid userContactId): IRequest<bool>
{
    [DataMember] public Guid UserId { get; private set; } = userId;

    [DataMember] public Guid UserContactId { get; private set; } = userContactId;
}
