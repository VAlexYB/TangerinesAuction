#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TangerinesAuction.Web/TangerinesAuction.Web.csproj", "TangerinesAuction.Web/"]
COPY ["TangerineAuction.Infrastructure/TangerineAuction.Infrastructure.csproj", "TangerineAuction.Infrastructure/"]
COPY ["TangerinesAuction.Core/TangerinesAuction.Core.csproj", "TangerinesAuction.Core/"]
COPY ["TangerinesAuction.Application/TangerinesAuction.Application.csproj", "TangerinesAuction.Application/"]
RUN dotnet restore "./TangerinesAuction.Web/./TangerinesAuction.Web.csproj"
COPY . .
WORKDIR "/src/TangerinesAuction.Web"
RUN dotnet build "./TangerinesAuction.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TangerinesAuction.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TangerinesAuction.Web.dll"]