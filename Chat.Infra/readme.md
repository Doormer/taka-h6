# Chat.Infra Project

## Overview

The `Chat.Infra` project is part of the Chat application infrastructure. It contains the Entity Framework Core
configurations, repositories, and the `ChatContext` class which is the database context for the application.

## Adding Migrations

To add migrations, use the following command inside the `Chat.Infra` project directory:

```sh
dotnet ef migrations add {migrationName} --startup-project ../Chat.ApiService
```

Replace `{migrationName}` with the name of your migration.

## Run Migration against database

To run migrations, use the following command inside the `Chat.Infra` project directory:

```sh
dotnet ef database update --startup-project ../Chat.ApiService --configuration Debug
```

## Project Structure

* Chat.Infra/ChatContext.cs: Contains the ChatContext class which is the database context.
* Chat.Infra/EntityConfigurations/: Contains entity type configurations.
* Chat.Infra/Repositories/: Contains repository implementations.