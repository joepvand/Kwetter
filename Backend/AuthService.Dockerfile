#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Auth/AuthService.Api/AuthService.Api.csproj", "Auth/AuthService.Api/"]
COPY ["Auth/AuthService.Data/AuthService.Data.csproj", "Auth/AuthService.Data/"]
COPY ["Auth/AuthService.Model/AuthService.Model.csproj", "Auth/AuthService.Model/"]
COPY ["Auth/AuthService.Application/AuthService.Application.csproj", "Auth/AuthService.Application/"]
RUN dotnet restore "Auth/AuthService.Api/AuthService.Api.csproj"
COPY . .
WORKDIR "/src/Auth/AuthService.Api"
RUN dotnet build "AuthService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.Api.dll"]