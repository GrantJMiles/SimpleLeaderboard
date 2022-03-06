FROM mcr.microsoft.com/dotnet/aspnet:5.0.14-bullseye-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/SimpleLeaderboard.Application"
RUN dotnet restore "SimpleLeaderboard.Application.csproj" -r linux-arm 
RUN dotnet build "SimpleLeaderboard.Application.csproj" -c Release -r linux-arm  -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "SimpleLeaderboard.Application.csproj" -c Release -o /app/publish -r linux-arm --self-contained true --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleLeaderboard.Application.dll"]
