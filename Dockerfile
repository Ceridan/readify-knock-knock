FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build
WORKDIR /src
COPY ["src/Readify.KnockKnock.Api/Readify.KnockKnock.Api.csproj", "Readify.KnockKnock.Api/"]
RUN dotnet restore "Readify.KnockKnock.Api/Readify.KnockKnock.Api.csproj"
COPY src/ .
WORKDIR "/src/Readify.KnockKnock.Api"
RUN dotnet build "Readify.KnockKnock.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Readify.KnockKnock.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Readify.KnockKnock.Api.dll"]
