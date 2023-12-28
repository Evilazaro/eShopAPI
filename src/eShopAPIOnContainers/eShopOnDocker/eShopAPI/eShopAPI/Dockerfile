#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

#src/eShopAPIOnContainers/eShopOnDocker/eShopAPI/eShopAPI/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY . .

RUN dotnet restore "./eShopAPI/eShopAPI.csproj"
RUN dotnet build "./eShopAPI/eShopAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet test "./eShopAPITests/eShopAPITests.csproj" --logger trx --results-directory "TestResults-8.0"

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./eShopAPI/eShopAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopAPI.dll"]