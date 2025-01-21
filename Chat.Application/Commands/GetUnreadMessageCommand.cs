using MediatR;
using System.Runtime.Serialization;

namespace Chat.ApiService.Application.Commands;

[DataContract]
public class GetUnreadMessageCommand(Guid contactId) : IRequest<int>
{
    [DataMember] public Guid ContactId { get; private set; } = contactId;
}