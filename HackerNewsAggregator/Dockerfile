FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HackerNewsAggregator/HackerNewsAggregator.csproj", "HackerNewsAggregator/"]
RUN dotnet restore "HackerNewsAggregator/HackerNewsAggregator.csproj"
COPY . .
WORKDIR "/src/HackerNewsAggregator"
RUN dotnet build "HackerNewsAggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HackerNewsAggregator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HackerNewsAggregator.dll"]
