using Chat.ApiService.Application.Behaviors;
using Chat.ApiService.Application.Commands;
using Chat.Application.Behaviors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Builder;
using MediatR;

namespace Chat.ApiService.Apis;

public class ChatEndpointServices
{
    public required IDistributedCache Cache { get; init; }
    public required IMediator Mediator { get; init; }
    public required ILogger<ChatEndpointServices> Logger { get; init; }
}

public static class ChatApi
{
    public static RouteGroupBuilder MapChatApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/v1/chat")
                    .WithTags("Chat")
                    .WithOpenApi();

        api.MapPost("/contact", AddContactAsync);

        return api;
    }
    
    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> AddContactAsync(
        [FromHeader(Name = "x-requestid")] Guid requestId,
        AddContactCommand command,
        [AsParameters] ChatEndpointServices services)
    {
        if (requestId == Guid.Empty)
        {
            return TypedResults.BadRequest("Empty GUID is not valid for request ID");
        }

        var idempotencyKey = $"contact-add-{requestId}";

        if (await services.Cache.GetAsync(idempotencyKey) != null)
        {
            return TypedResults.Ok();
        }

        var requestAddContact = new AddContactCommand(command.UserId, command.UserContactId);

        services.Logger.LogInformation(
            "Sending command: {CommandName} ({@Command})",
            requestAddContact.GetGenericTypeName(),
            requestAddContact);

        var commandResult = await services.Mediator.Send(requestAddContact);

        if (!commandResult)
        {
            return TypedResults.Problem(detail: "Add contact failed to process.", statusCode: 500);
        }

        await services.Cache.SetAsync(
            idempotencyKey,
            [1],
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });

        return TypedResults.Ok();
    }
}