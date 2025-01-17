using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Chat.Application.Commands.SendMessage;
using Chat.Application.Commands.MarkMessageAsRead;
using Chat.Application.Commands.MarkMessageAsDelivered;
using Chat.Application.Queries.GetMessages;
using Microsoft.AspNetCore.Http;
using Chat.Application.Commands.UpdateMessageStatus;

namespace Chat.ApiService.Apis;

public static class MessageApi
{
    public static IEndpointRouteBuilder MapMessageApiV1(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/messages")
            .WithTags("Messages")
            .WithOpenApi();

        // 获取消息历史
        group.MapGet("/", async (
            [FromQuery] Guid userId,
            [FromQuery] Guid contactId,
            [FromQuery] DateTime? cursor,
            [FromQuery] int? limit,
            ISender sender,
            CancellationToken ct) =>
        {
            var query = new GetMessagesQuery
            {
                UserId = userId,
                ContactId = contactId,
                Cursor = cursor,
                Limit = limit ?? 20
            };

            var messages = await sender.Send(query, ct);
            return TypedResults.Ok(messages);
        })
        .WithName("GetMessages")
        .WithDescription("获取聊天消息历史");

        // 更新消息状态
        group.MapPut("/{messageId}/status", async (
            Guid messageId,
            [FromBody] MessageStatus newStatus,
            [FromHeader] Guid userId,
            ISender sender,
            CancellationToken ct) =>
        {
            var command = new UpdateMessageStatusCommand
            {
                MessageId = messageId,
                UserId = userId,
                NewStatus = newStatus
            };

            var result = await sender.Send(command, ct);
            return result ? TypedResults.Ok() : TypedResults.BadRequest();
        })
        .WithName("UpdateMessageStatus")
        .WithDescription("更新消息状态（已送达/已读）");

        // 发送消息
        group.MapPost("/", async Task<Results<Ok<Guid>, BadRequest>> (
            SendMessageCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            try 
            {
                var messageId = await sender.Send(command, ct);
                return TypedResults.Ok(messageId);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest();
            }
        })
        .WithName("SendMessage")
        .WithDescription("发送消息");

        // 获取用户间的消息历史
        group.MapGet("/{user1Id}/{user2Id}", async Task<Results<Ok<List<MessageDto>>, BadRequest>> (
            Guid user1Id,
            Guid user2Id,
            [AsParameters] GetMessagesQuery query,
            ISender sender,
            CancellationToken ct) =>
        {
            try
            {
                query.User1Id = user1Id;
                query.User2Id = user2Id;
                var messages = await sender.Send(query, ct);
                return TypedResults.Ok(messages);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest();
            }
        })
        .WithName("GetMessages")
        .WithDescription("获取用户间的消息历史");

        // 标记消息已读
        group.MapPut("/{messageId}/read", async Task<Results<Ok<bool>, NotFound, BadRequest>> (
            Guid messageId,
            Guid readerId,
            ISender sender,
            CancellationToken ct) =>
        {
            try
            {
                var command = new MarkMessageAsReadCommand(messageId, readerId);
                var result = await sender.Send(command, ct);
                return TypedResults.Ok(result);
            }
            catch (ChatNotFoundException)
            {
                return TypedResults.NotFound();
            }
            catch (Exception)
            {
                return TypedResults.BadRequest();
            }
        })
        .WithName("MarkMessageAsRead")
        .WithDescription("标记消息已读");

        // 标记消息已送达
        group.MapPut("/{messageId}/delivered", async Task<Results<Ok<bool>, NotFound, BadRequest>> (
            Guid messageId,
            Guid receiverId,
            ISender sender,
            CancellationToken ct) =>
        {
            try
            {
                var command = new MarkMessageAsDeliveredCommand(messageId, receiverId);
                var result = await sender.Send(command, ct);
                return TypedResults.Ok(result);
            }
            catch (ChatNotFoundException)
            {
                return TypedResults.NotFound();
            }
            catch (Exception)
            {
                return TypedResults.BadRequest();
            }
        })
        .WithName("MarkMessageAsDelivered")
        .WithDescription("标记消息已送达");

        // 发送文件消息
        group.MapPost("/file", async Task<Results<Ok<Guid>, BadRequest>> (
            [FromForm] IFormFile file,
            [FromForm] Guid senderId,
            [FromForm] Guid receiverId,
            ISender sender,
            CancellationToken ct) =>
        {
            try 
            {
                var command = new SendFileMessageCommand
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    File = file
                };
                
                var messageId = await sender.Send(command, ct);
                return TypedResults.Ok(messageId);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest();
            }
        })
        .WithName("SendFileMessage")
        .WithDescription("发送文件消息");

        return app;
    }
}

public class MessageDto
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Content { get; set; }
    public MessageType Type { get; set; }
    public string? FileUrl { get; set; }
    public string? FileName { get; set; }
    public MessageStatus Status { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? ReadTime { get; set; }
} 