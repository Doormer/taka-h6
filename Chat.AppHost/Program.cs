using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Chat_ApiService>("apiservice");

builder.Build().Run();