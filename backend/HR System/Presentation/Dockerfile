# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Common/Common/Common.csproj Common/Common/
COPY EmployeeService/Core/Core.csproj EmployeeService/Core/
COPY EmployeeService/Application/Application.csproj EmployeeService/Application/
COPY EmployeeService/Infrastructure/Infrastructure.csproj EmployeeService/Infrastructure/
COPY EmployeeService/Presentation/Presentation.csproj EmployeeService/Presentation/

# Restore dependencies
RUN dotnet restore EmployeeService/Presentation/Presentation.csproj

# Copy the rest of the code
COPY Common/Common/ Common/Common/
COPY EmployeeService/Core/. EmployeeService/Core/
COPY EmployeeService/Application/. EmployeeService/Application/
COPY EmployeeService/Infrastructure/. EmployeeService/Infrastructure/
COPY EmployeeService/Presentation/. EmployeeService/Presentation/

# Build the solution
RUN dotnet build EmployeeService/Presentation/Presentation.csproj -c Release -o /app/build

# Stage 2: Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /app

# Copy the build artifacts and project files
COPY --from=build /app/build /app/build
COPY --from=build /app/Common/Common /app/Common/Common
COPY --from=build /app/EmployeeService/Core /app/EmployeeService/Core
COPY --from=build /app/EmployeeService/Application /app/EmployeeService/Application
COPY --from=build /app/EmployeeService/Infrastructure /app/EmployeeService//Infrastructure
COPY --from=build /app/EmployeeService/Presentation /app/EmployeeService/Presentation

# Publish the project
RUN dotnet publish EmployeeService/Presentation/Presentation.csproj -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port and define entrypoint
EXPOSE 8081
ENTRYPOINT ["dotnet", "Presentation.dll"]