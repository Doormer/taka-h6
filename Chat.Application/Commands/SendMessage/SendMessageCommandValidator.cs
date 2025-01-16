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