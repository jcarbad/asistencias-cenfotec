# Use the official .NET image as a build stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Ausencias.API.csproj", "."]
RUN dotnet restore "Ausencias.API.csproj"

COPY . .
WORKDIR "/src/"
RUN dotnet build "Ausencias.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ausencias.API.csproj" -c Release -o /app/publish

# Use the base image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ausencias.API.dll"]