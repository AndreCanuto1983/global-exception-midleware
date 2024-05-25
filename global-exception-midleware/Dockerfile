FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["global-exception-midleware/global-exception-midleware.csproj", "global-exception-midleware/"]
RUN dotnet restore "./global-exception-midleware/global-exception-midleware.csproj"
COPY . .
WORKDIR "/src/global-exception-midleware"
RUN dotnet build "./global-exception-midleware.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./global-exception-midleware.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "global-exception-midleware.dll"]