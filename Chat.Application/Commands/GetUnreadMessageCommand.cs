using MediatR;
using System.Runtime.Serialization;

namespace Chat.ApiService.Application.Commands;

[DataContract]
public class GetUnreadMessageCommand(int contactId) : IRequest<int>
{
    [DataMember] public int ContactId { get; private set; } = contactId;
}