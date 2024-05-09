#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["app/app.csproj", "app/"]
RUN dotnet restore "./app/app.csproj"
COPY . .
WORKDIR "/src/app"
RUN dotnet build "./app.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS migrations
WORKDIR /app
RUN dotnet tool install --version 6.0.9 --global dotnet-ef
RUN dotnet ef migrations add InitialMigration --context ApplicationContext.cs --output-dir migrations

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./app.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY ./mysql-initdb/init.sql /docker-entrypoint-initdb.d/
WORKDIR /app/migrations
COPY --from=migration /app/migrations .

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "app.dll"]