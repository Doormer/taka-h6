using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Chat.Application.Common.Interfaces;
using Chat.Domain.AggregateModels.MessageAggregate;

namespace Chat.Application.Commands.SendMessage
{
    public class SendFileMessageCommand : IRequest<Guid>
    {
        public Guid SenderId { get; init; }
        public Guid ReceiverId { get; init; }
        public IFormFile File { get; init; }
    }

    public class SendFileMessageCommandHandler : IRequestHandler<SendFileMessageCommand, Guid>
    {
        private readonly IFileStorageService _fileStorage;
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SendFileMessageCommandHandler(
            IFileStorageService fileStorage,
            IMessageRepository messageRepository,
            IUnitOfWork unitOfWork)
        {
            _fileStorage = fileStorage;
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(SendFileMessageCommand request, CancellationToken cancellationToken)
        {
            // Upload file to blob storage
            using var stream = request.File.OpenReadStream();
            var fileUrl = await _fileStorage.UploadFileAsync(
                stream,
                request.File.FileName,
                request.File.ContentType);

            // Create message with file content
            var content = MessageContent.CreateFileContent(
                fileUrl,
                request.File.FileName,
                request.File.ContentType);

            var message = Message.Create(
                request.SenderId,
                request.ReceiverId,
                content);

            _messageRepository.Add(message);
            await _unitOfWork.SaveEntitiesAsync(cancellationToken);

            return message.Id;
        }
    }
} 