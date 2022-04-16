#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CommentService/CommentService.Api/CommentService.Api.csproj", "CommentService/CommentService.Api/"]
COPY ["CommentService/CommentService.Data/CommentService.Data.csproj", "CommentService/CommentService.Data/"]
COPY ["CommentService/CommentService.DomainModels/CommentService.DomainModels.csproj", "CommentService/CommentService.DomainModels/"]
COPY ["CommentService/CommentService.Application/CommentService.Application.csproj", "CommentService/CommentService.Application/"]
COPY ["Helpers/Helpers.csproj", "Helpers/"]
RUN dotnet restore "CommentService/CommentService.Api/CommentService.Api.csproj"
COPY . .
WORKDIR "/src/CommentService/CommentService.Api"
RUN dotnet build "CommentService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CommentService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommentService.Api.dll"]