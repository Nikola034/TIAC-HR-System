# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Common/Common.csproj Common/
COPY IdentityService.Core/IdentityService.Core.csproj IdentityService.Core/
COPY IdentityService.Application/IdentityService.Application.csproj IdentityService.Application/
COPY IdentityService.Infrastructure/IdentityService.Infrastructure.csproj IdentityService.Infrastructure/
COPY IdentityService.Presentation/IdentityService.Presentation.csproj IdentityService.Presentation/

# Restore dependencies
RUN dotnet restore IdentityService.Presentation/IdentityService.Presentation.csproj

# Copy the rest of the code
COPY Common/ Common/
COPY IdentityService.Core/. IdentityService.Core/
COPY IdentityService.Application/. IdentityService.Application/
COPY IdentityService.Infrastructure/. IdentityService.Infrastructure/
COPY IdentityService.Presentation/. IdentityService.Presentation/

# Build the solution
RUN dotnet build IdentityService.Presentation/IdentityService.Presentation.csproj -c Release -o /app/build

# Stage 2: Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /app

# Copy the build artifacts and project files
COPY --from=build /app/build /app/build
COPY --from=build /app/Common /app/Common
COPY --from=build /app/IdentityService.Core /app/IdentityService.Core
COPY --from=build /app/IdentityService.Application /app/IdentityService.Application
COPY --from=build /app/IdentityService.Infrastructure /app/IdentityService.Infrastructure
COPY --from=build /app/IdentityService.Presentation /app/IdentityService.Presentation

# Publish the project
RUN dotnet publish IdentityService.Presentation/IdentityService.Presentation.csproj -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port and define entrypoint
EXPOSE 8082
ENTRYPOINT ["dotnet", "IdentityService.Presentation.dll"]