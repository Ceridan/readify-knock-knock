FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["src/Readify.KnockKnock.Api/Readify.KnockKnock.Api.csproj", "Readify.KnockKnock.Api/"]
RUN dotnet restore "Readify.KnockKnock.Api/Readify.KnockKnock.Api.csproj"
COPY ["src/Readify.KnockKnock.UnitTests/Readify.KnockKnock.UnitTests.csproj", "Readify.KnockKnock.UnitTests/"]
RUN dotnet restore "Readify.KnockKnock.UnitTests/Readify.KnockKnock.UnitTests.csproj"
COPY ["src/Readify.KnockKnock.IntegrationTests/Readify.KnockKnock.IntegrationTests.csproj", "Readify.KnockKnock.IntegrationTests/"]
RUN dotnet restore "Readify.KnockKnock.IntegrationTests/Readify.KnockKnock.IntegrationTests.csproj"
COPY src/ .

WORKDIR "/src/Readify.KnockKnock.Api"
RUN dotnet build --no-restore "Readify.KnockKnock.Api.csproj" --configuration Release --output /app

WORKDIR "/src/Readify.KnockKnock.UnitTests"
RUN dotnet build --no-restore "Readify.KnockKnock.UnitTests.csproj"
RUN dotnet test "Readify.KnockKnock.UnitTests.csproj"

WORKDIR "/src/Readify.KnockKnock.IntegrationTests"
RUN dotnet build --no-restore "Readify.KnockKnock.IntegrationTests.csproj"
RUN dotnet test "Readify.KnockKnock.IntegrationTests.csproj"

FROM build AS publish
RUN dotnet publish "Readify.KnockKnock.Api.csproj" --configuration Release --output /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Readify.KnockKnock.Api.dll"]
