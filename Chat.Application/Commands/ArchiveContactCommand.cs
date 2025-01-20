using MediatR;
using System.Runtime.Serialization;

namespace Chat.Application.Commands;

[DataContract]
public class ArchiveContactCommand : IRequest<Unit>
{
    [DataMember] public Guid UserId { get; set; }
    [DataMember] public Guid ContactUserId { get; set; }
}