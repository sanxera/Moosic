#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MSC.SentimentAnalysis.API/MSC.SentimentAnalysis.API.csproj", "MSC.SentimentAnalysis.API/"]
RUN dotnet restore "MSC.SentimentAnalysis.API/MSC.SentimentAnalysis.API.csproj"
COPY . .
WORKDIR "/src/MSC.SentimentAnalysis.API"
RUN dotnet build "MSC.SentimentAnalysis.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSC.SentimentAnalysis.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSC.SentimentAnalysis.API.dll"]