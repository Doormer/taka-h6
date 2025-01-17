using Chat.ApiService.Application.Behaviors;
using Chat.ApiService.Application.Commands;
using Chat.Application.Behaviors;
using Chat.Application.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Chat.ApiService.Apis;

public static class ChatApi
{
    public static RouteGroupBuilder MapChatApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/chat")
            .WithTags("Chat")
            .WithOpenApi();

        api.MapPost("/contact", AddContactAsync)
            .WithName("AddContact")
            .WithDescription("Add a new contact");

        api.MapGet("/contacts/{userId}", GetContactsAsync)
            .WithName("GetContacts")
            .WithDescription("Get all contacts of a user");

        return api;
    }

    private static async Task<IResult> GetContactsAsync(
        string userId,
        IChatQueries queries,
        ILogger<Program> logger)
    {
        logger.LogInformation("Getting contacts for user: {UserId}", userId);
        try
        {
            var contacts = await queries.GetContactsAsync(userId);
            logger.LogInformation("Found {Count} contacts for user {UserId}", contacts.Count, userId);
            return Results.Ok(contacts);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting contacts for user {UserId}", userId);
            return Results.Problem(detail: ex.Message);
        }
    }

    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> AddContactAsync(
        AddContactCommand command,
        [AsParameters] ChatServices services)
    {
        services.Logger.LogInformation(
            "Adding contact - UserId: {UserId}, ContactUserId: {ContactUserId}",
            command.UserId,
            command.UserContactId);

        var requestAddContact = new AddContactCommand(command.UserId, command.UserContactId);
        var commandResult = await services.Mediator.Send(requestAddContact);

        if (!commandResult)
        {
            services.Logger.LogError("Failed to add contact");
            return TypedResults.Problem(detail: "Add contact failed to process.", statusCode: 500);
        }

        services.Logger.LogInformation("Contact added successfully");
        return TypedResults.Ok();
    }
}