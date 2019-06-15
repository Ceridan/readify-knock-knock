FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build
WORKDIR /src
COPY ["src/Readify.KnockKnock.Api/Readify.KnockKnock.Api.csproj", "Readify.KnockKnock.Api/"]
RUN dotnet restore "Readify.KnockKnock.Api/Readify.KnockKnock.Api.csproj"
COPY ["src/Readify.KnockKnock.UnitTests/Readify.KnockKnock.UnitTests.csproj", "Readify.KnockKnock.UnitTests/"]
RUN dotnet restore "Readify.KnockKnock.UnitTests/Readify.KnockKnock.UnitTests.csproj"
COPY src/ .

WORKDIR "/src/Readify.KnockKnock.UnitTests"
RUN dotnet build --no-restore "Readify.KnockKnock.UnitTests.csproj"
RUN dotnet test "Readify.KnockKnock.UnitTests.csproj"

WORKDIR "/src/Readify.KnockKnock.Api"
RUN dotnet build --no-restore "Readify.KnockKnock.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Readify.KnockKnock.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Readify.KnockKnock.Api.dll"]
