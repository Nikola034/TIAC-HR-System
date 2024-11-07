FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY APIGateway/*.csproj ./

RUN dotnet restore

COPY APIGateway/ ./

RUN dotnet build -c Release -o out

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out .

EXPOSE 8080

# Define the entry point for the container
ENTRYPOINT ["dotnet", "APIGateway.dll"]

