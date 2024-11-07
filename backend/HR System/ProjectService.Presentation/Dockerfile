# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Common/Common.csproj Common/
COPY ProjectService.Core/ProjectService.Core.csproj ProjectService.Core/
COPY ProjectService.Application/ProjectService.Application.csproj ProjectService.Application/
COPY ProjectService.Infrastructure/ProjectService.Infrastructure.csproj ProjectService.Infrastructure/
COPY ProjectService.Presentation/ProjectService.Presentation.csproj ProjectService.Presentation/

# Restore dependencies
RUN dotnet restore ProjectService.Presentation/ProjectService.Presentation.csproj

# Copy the rest of the code
COPY Common/ Common/
COPY ProjectService.Core/. ProjectService.Core/
COPY ProjectService.Application/. ProjectService.Application/
COPY ProjectService.Infrastructure/. ProjectService.Infrastructure/
COPY ProjectService.Presentation/. ProjectService.Presentation/

# Build the solution
RUN dotnet build ProjectService.Presentation/ProjectService.Presentation.csproj -c Release -o /app/build

# Stage 2: Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /app

# Copy the build artifacts and project files
COPY --from=build /app/build /app/build
COPY --from=build /app/Common /app/Common
COPY --from=build /app/ProjectService.Core /app/ProjectService.Core
COPY --from=build /app/ProjectService.Application /app/ProjectService.Application
COPY --from=build /app/ProjectService.Infrastructure /app/ProjectService.Infrastructure
COPY --from=build /app/ProjectService.Presentation /app/ProjectService.Presentation

# Publish the project
RUN dotnet publish ProjectService.Presentation/ProjectService.Presentation.csproj -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port and define entrypoint
EXPOSE 8083
ENTRYPOINT ["dotnet", "ProjectService.Presentation.dll"]