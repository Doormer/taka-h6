using Chat.ApiService.Application.Behaviors;
using Chat.ApiService.Application.Commands;
using Chat.Application.Behaviors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Chat.ApiService.Apis;

public static class ChatApi
{
    public static RouteGroupBuilder MapChatApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/chat");
        //todo
        //setup API versioning

        api.MapPost("/contact", AddContactAsync);

        return api;
    }
    
    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> AddContactAsync(
        //TODO handle idempotency [FromHeader(Name = "x-requestid")] Guid requestId,
        AddContactCommand command,
        [AsParameters] ChatServices services)
    {
        //TODO handle idempotency
        // if (requestId == Guid.Empty)
        // {
        //     return TypedResults.BadRequest("Empty GUID is not valid for request ID");
        // }

        var requestAddContact= new AddContactCommand(command.UserId, command.UserContactId);

        services.Logger.LogInformation(
            "Sending command: {CommandName} ({@Command})",
            requestAddContact.GetGenericTypeName(),
            requestAddContact);

        var commandResult = await services.Mediator.Send(requestAddContact);

        if (!commandResult)
        {
            return TypedResults.Problem(detail: "Add contact failed to process.", statusCode: 500);
        }

        return TypedResults.Ok();
    }
}