FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Chat.ApiService/Chat.ApiService.csproj", "Chat.ApiService/"]
COPY ["Chat.Application/Chat.Application.csproj", "Chat.Application/"]
COPY ["Chat.Domain/Chat.Domain.csproj", "Chat.Domain/"]
COPY ["Chat.Infra/Chat.Infra.csproj", "Chat.Infra/"]
RUN dotnet restore "Chat.ApiService/Chat.ApiService.csproj"
COPY . .
WORKDIR "/src/Chat.ApiService"
RUN dotnet build "Chat.ApiService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chat.ApiService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chat.ApiService.dll"] 