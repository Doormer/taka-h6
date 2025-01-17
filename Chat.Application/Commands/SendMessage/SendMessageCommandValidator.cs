using FluentValidation;

namespace Chat.Application.Commands.SendMessage;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("发送者ID不能为空");

        RuleFor(x => x.ReceiverId)
            .NotEmpty().WithMessage("接收者ID不能为空")
            .NotEqual(x => x.SenderId).WithMessage("不能给自己发送消息");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("消息内容不能为空")
            .MaximumLength(5000).WithMessage("消息内容不能超过5000个字符");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("无效的消息类型");
    }
}

public class SendFileMessageCommandValidator : AbstractValidator<SendFileMessageCommand>
{
    private const int MaxFileSizeInMb = 10;
    private readonly string[] AllowedFileTypes = new[] 
    { 
        "image/jpeg", 
        "image/png", 
        "application/pdf",
        "application/msword",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
    };

    public SendFileMessageCommandValidator()
    {
        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("发送者ID不能为空");

        RuleFor(x => x.ReceiverId)
            .NotEmpty().WithMessage("接收者ID不能为空")
            .NotEqual(x => x.SenderId).WithMessage("不能给自己发送消息");

        RuleFor(x => x.File)
            .NotNull().WithMessage("文件不能为空")
            .Must(file => file.Length <= MaxFileSizeInMb * 1024 * 1024)
                .WithMessage($"文件大小不能超过{MaxFileSizeInMb}MB")
            .Must(file => AllowedFileTypes.Contains(file.ContentType.ToLower()))
                .WithMessage("不支持的文件类型");
    }
} 