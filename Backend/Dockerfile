# Use the .NET 8 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore

# Build the project
RUN dotnet publish -c Release -o /publish

# Use the .NET 8 runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

# Expose the port and run the application
EXPOSE 5432
ENTRYPOINT ["dotnet", "Anonymous Chat.dll"]
