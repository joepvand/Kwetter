#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TweetService/TweetService.Api/TweetService.Api.csproj", "TweetService/TweetService.Api/"]
COPY ["TweetService/TweetService.Model/TweetService.DomainModel.csproj", "TweetService/TweetService.Model/"]
COPY ["Helpers/Helpers.csproj", "Helpers/"]
COPY ["TweetService/TweetService.Data/TweetService.Data.csproj", "TweetService/TweetService.Data/"]
COPY ["MessagingModels/MessagingModels.csproj", "MessagingModels/"]
COPY ["TweetService/TweetService.Application/TweetService.Application.csproj", "TweetService/TweetService.Application/"]
RUN dotnet restore "TweetService/TweetService.Api/TweetService.Api.csproj"
COPY . .
WORKDIR "/src/TweetService/TweetService.Api"
RUN dotnet build "TweetService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TweetService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TweetService.Api.dll"]