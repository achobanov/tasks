FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY /src/Web/Challenge.Web.API/Challenge.Web.API.csproj           Web/Challenge.Web.API/
COPY /src/Web/Challenge.Web.Client/Challenge.Web.Client.csproj     Web/Challenge.Web.Client/
COPY /src/Web/Challenge.Web.Common/Challenge.Web.Common.csproj     Web/Challenge.Web.Common/
COPY /src/Challenge.Common/Challenge.Common.csproj                 Challenge.Common/
COPY /src/Challenge.Domain/Challenge.Domain.csproj                 Challenge.Domain/

RUN dotnet restore Web/Challenge.Web.API/Challenge.Web.API.csproj

COPY src/Web/Challenge.Web.API         Web/Challenge.Web.API
COPY src/Web/Challenge.Web.Client      Web/Challenge.Web.Client
COPY src/Web/Challenge.Web.Common      Web/Challenge.Web.Common
COPY src/Challenge.Common              Challenge.Common
COPY src/Challenge.Domain              Challenge.Domain

WORKDIR /test
COPY /test/Challenge.Tests   Challenge.Tests

RUN dotnet test Challenge.Tests/Challenge.Tests.csproj

WORKDIR /src
RUN dotnet build Web/Challenge.Web.API/Challenge.Web.API.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Web/Challenge.Web.API/Challenge.Web.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app/publish
COPY --from=publish /app/publish .

ENTRYPOINT dotnet Challenge.Web.API.dll