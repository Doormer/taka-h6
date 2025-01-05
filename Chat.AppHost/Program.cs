var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Chat_ApiService>("apiservice");

builder.Build().Run();
