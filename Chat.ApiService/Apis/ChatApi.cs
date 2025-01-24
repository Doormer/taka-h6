using Chat.ApiService.Application.Behaviors;
using Chat.ApiService.Application.Commands;
using Chat.Application.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Contact = Chat.Application.Queries.Contact;

namespace Chat.ApiService.Apis;

public static class ChatApi
{
    public static RouteGroupBuilder MapChatApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/chat");

        //todo
        //setup API versioning

        api.MapPost("/get-active-contacts", GetActiveContactsAsync);
        api.MapPost("/get-archived-contact", GetArchivedContactAsync);
        api.MapPost("/create-contact", AddContactAsync);
        api.MapPost("/archive-contact", ArchiveContactAsync);
        api.MapPost("/unarchive-contact", UnarchiveContactAsync);

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
        // Domain drive design + Command Query responsibility Seperation (CQRS 

        var requestAddContact = new AddContactCommand
            { UserId = command.UserId, ContactUserId = command.ContactUserId };

        services.Logger.LogInformation(
            "Sending command: {CommandName} ({@Command})",
            requestAddContact.GetGenericTypeName(),
            requestAddContact);

        await services.Mediator.Send(requestAddContact);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> ArchiveContactAsync(
        ArchiveContactCommand command,
        [AsParameters] ChatServices services)
    {
        try
        {
            services.Logger.LogInformation(
                "Receiving archive command for userId: {UserId}, contactUserId: {ContactUserId}",
                command.UserId,
                command.ContactUserId);

            await services.Mediator.Send(command);
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            services.Logger.LogError(ex, "Error processing archive contact");
            return TypedResults.Problem(ex.Message, statusCode: 500);
        }
    }

    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> UnarchiveContactAsync(
        UnarchiveContactCommand command,
        [AsParameters] ChatServices services)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} ({@Command})",
            command.GetGenericTypeName(),
            command);

        await services.Mediator.Send(command);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok<List<Contact>>, NotFound>> GetActiveContactsAsync(
        Guid userId,
        [AsParameters] ChatServices services)
    {
        //todo
        // get userId from token
        try
        {
            var users = await services.Queries.GetActiveContactsAsync(userId);
            return TypedResults.Ok(users);
        }
        catch
        {
            return TypedResults.NotFound();
        }
    }

    public static async Task<Results<Ok<List<Contact>>, NotFound>> GetArchivedContactAsync(
        Guid userId,
        [AsParameters] ChatServices services)
    {
        try
        {
            var contact = await services.Queries.GetArchivedContactsAsync(userId);
            return TypedResults.Ok(contact);
        }
        catch
        {
            return TypedResults.NotFound();
        }
    }
}