using Chat.ApiService.Application.Behaviors;
using Chat.ApiService.Application.Commands;
using Chat.Domain.AggregateModels.MessageAggregate;
using Microsoft.AspNetCore.Http.HttpResults;
using Nest;
namespace Chat.ApiService.Apis;

public static class ChatApi
{
    public static RouteGroupBuilder MapChatApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/chat");

        //todo
        //setup API versioning

        api.MapPost("/create-contact", AddContactAsync);
        api.MapPost("/archive-contact", ArchiveContactAsync);
        
        api.MapPost("/query-message", QueryMessageAsync);

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

        var requestAddContact = new AddContactCommand(command.UserId, command.UserContactId);

        services.Logger.LogInformation(
            "Sending command: {CommandName} ({@Command})",
            requestAddContact.GetGenericTypeName(),
            requestAddContact);

        var commandResult = await services.Mediator.Send(requestAddContact);

        if (!commandResult)
        {
            return TypedResults.Problem("Add contact failed to process.", statusCode: 500);
        }

        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> ArchiveContactAsync(

        //TODO handle idempotency [FromHeader(Name = "x-requestid")] Guid requestId,
        ArchiveContactCommand command,
        [AsParameters] ChatServices services)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} ({@Command})",
            command.GetGenericTypeName(),
            command);

        var commandResult = await services.Mediator.Send(command);

        if (!commandResult)
        {
            return TypedResults.Problem("Add contact failed to process.", statusCode: 500);
        }

        return TypedResults.Ok();
    }
    
    public static async Task<List<Message>> QueryMessageAsync(

        //TODO handle idempotency [FromHeader(Name = "x-requestid")] Guid requestId,
        QueryMessageCommand command,
        [AsParameters] ChatServices services)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} ({@Command})",
            command.GetGenericTypeName(),
            command);

        var settings = new ConnectionSettings(new Uri("http://localhost:9200/"))
            .DefaultIndex("message")
            //.BasicAuthentication("elastic", "123456")                 
            .ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true)
            .EnableDebugMode();
        var client = new ElasticClient(settings);
        var clusterHealth = client.Cluster.Health();
        if (clusterHealth.IsValid)
        {
            //开始执行操作
            Console.WriteLine($"ElasticSearch连接成功");
        }
        else
        {
            Console.WriteLine($"ElasticSearch连接失败{clusterHealth.OriginalException.Message}");
        }
        
        var searchRequest = new SearchRequest<Message>
        {   
            From = 0,
            Size = 5000,
            Query = new BoolQuery
            {
                Must = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = Infer.Field<Message>(f => f.Content),
                        Query = command.Keyword
                    },
                    new BoolQuery
                    {
                        Should = new List<QueryContainer>
                        {
                            new TermQuery
                            {
                                Field = Infer.Field<Message>(f => f.SenderId),
                                
                                Value = command.UserId.ToString()
                            },
                            new TermQuery
                            {
                                Field = Infer.Field<Message>(f => f.ReceiverId),
                                Value = command.UserId.ToString()
                            }
                        }
                    }
                },
                
            }
        };
        
        var response = await client.SearchAsync<Message>(searchRequest);
        var debugInformation = response.DebugInformation;
        Console.WriteLine(debugInformation);
        
        if (response.IsValid)
        {
            return response.Documents.ToList();
        }
        else
        {
            throw new Exception("Error occurred while querying Elasticsearch");
        }
    }
}