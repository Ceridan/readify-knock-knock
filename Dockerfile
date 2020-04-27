
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src

COPY src/ .

RUN dotnet restore "Readify.KnockKnock.Api/Readify.KnockKnock.Api.csproj"
RUN dotnet restore "Readify.KnockKnock.UnitTests/Readify.KnockKnock.UnitTests.csproj"
RUN dotnet restore "Readify.KnockKnock.IntegrationTests/Readify.KnockKnock.IntegrationTests.csproj"

RUN dotnet build --no-restore "Readify.KnockKnock.Api/Readify.KnockKnock.Api.csproj" --configuration Release

RUN dotnet build --no-restore "Readify.KnockKnock.UnitTests/Readify.KnockKnock.UnitTests.csproj"
RUN dotnet test "Readify.KnockKnock.UnitTests/Readify.KnockKnock.UnitTests.csproj"

RUN dotnet build --no-restore "Readify.KnockKnock.IntegrationTests/Readify.KnockKnock.IntegrationTests.csproj"
RUN dotnet test "Readify.KnockKnock.IntegrationTests/Readify.KnockKnock.IntegrationTests.csproj"

RUN dotnet publish "Readify.KnockKnock.Api/Readify.KnockKnock.Api.csproj" --no-restore --no-build --configuration Release --output /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app .

EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Readify.KnockKnock.Api.dll"]
