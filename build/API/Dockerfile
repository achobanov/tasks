FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY /src/VL.Challenge.API/VL.Challenge.API.csproj                       VL.Challenge.API/
COPY /src/VL.Challenge.Blazor.Client/VL.Challenge.Blazor.Client.csproj   VL.Challenge.Blazor.Client/
COPY /src/VL.Challenge.Common/VL.Challenge.Common.csproj                 VL.Challenge.Common/
COPY /src/VL.Challenge.Domain/VL.Challenge.Domain.csproj                 VL.Challenge.Domain/
COPY /src/VL.Challenge.Storage/VL.Challenge.Storage.csproj               VL.Challenge.Storage/

RUN dotnet restore VL.Challenge.API/VL.Challenge.API.csproj

COPY src/VL.Challenge.API             VL.Challenge.API
COPY src/VL.Challenge.Blazor.Client   VL.Challenge.Blazor.Client
COPY src/VL.Challenge.Common          VL.Challenge.Common
COPY src/VL.Challenge.Domain          VL.Challenge.Domain
COPY src/VL.Challenge.Storage         VL.Challenge.Storage

WORKDIR /test
COPY /test/VL.Challenge.Tests   VL.Challenge.Tests

RUN dotnet test VL.Challenge.Tests/VL.Challenge.Tests.csproj

WORKDIR /src
RUN dotnet build VL.Challenge.API/VL.Challenge.API.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish VL.Challenge.API/VL.Challenge.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app/publish
COPY --from=publish /app/publish .

ENTRYPOINT dotnet VL.Challenge.API.dll