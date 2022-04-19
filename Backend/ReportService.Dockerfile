#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ReportService/ReportService.Api/ReportService.Api.csproj", "ReportService/ReportService.Api/"]
COPY ["ReportService/ReportService.Data/ReportService.Data.csproj", "ReportService/ReportService.Data/"]
COPY ["ReportService/ReportService.DomainModels/ReportService.DomainModels.csproj", "ReportService/ReportService.DomainModels/"]
COPY ["ReportService/ReportService.Application/ReportService.Application.csproj", "ReportService/ReportService.Application/"]
COPY ["Helpers/Helpers.csproj", "Helpers/"]
RUN dotnet restore "ReportService/ReportService.Api/ReportService.Api.csproj"
COPY . .
WORKDIR "/src/ReportService/ReportService.Api"
RUN dotnet build "ReportService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.Api.dll"]