FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY /src/Challenge.Web.API/Challenge.Web.API.csproj               Challenge.Web.API/
COPY /src/Challenge.Web.Client/Challenge.Web.Client.csproj         Challenge.Web.Client/
COPY /src/Challenge.Common/Challenge.Common.csproj                 Challenge.Common/
COPY /src/Challenge.Domain/Challenge.Domain.csproj                 Challenge.Domain/
COPY /src/Challenge.Storage/Challenge.Storage.csproj               Challenge.Storage/

RUN dotnet restore Challenge.Web.API/Challenge.Web.API.csproj

COPY src/Challenge.Web.API             Challenge.Web.API
COPY src/Challenge.Web.Client   Challenge.Web.Client
COPY src/Challenge.Common          Challenge.Common
COPY src/Challenge.Domain          Challenge.Domain
COPY src/Challenge.Storage         Challenge.Storage

WORKDIR /test
COPY /test/Challenge.Tests   Challenge.Tests

RUN dotnet test Challenge.Tests/Challenge.Tests.csproj

WORKDIR /src
RUN dotnet build Challenge.Web.API/Challenge.Web.API.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Challenge.Web.API/Challenge.Web.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app/publish
COPY --from=publish /app/publish .

ENTRYPOINT dotnet Challenge.Web.API.dll