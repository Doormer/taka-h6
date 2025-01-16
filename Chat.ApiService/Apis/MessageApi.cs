using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Chat.Application.Commands.SendMessage;
using Chat.Application.Commands.MarkMessageAsRead;
using Chat.Application.Commands.MarkMessageAsDelivered;
using Chat.Application.Queries.GetMessages;

namespace Chat.ApiService.Apis;

public static class MessageApi
{
    public static IEndpointRouteBuilder MapMessageApiV1(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/messages")
            .WithTags("Messages")
            .WithOpenApi();

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

        return app;
    }
} 