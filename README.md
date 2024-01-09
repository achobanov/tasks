![Ventures Lab](./docs/VL_signature.jpg)

# Solution
Alex's crack at the challenge

## Instructions
The solution relies on Docker (tested with latest version)

Run `start.bat` in the *repository root* directory

This script intilizes the application:
    1. Brings docker containers up
    2. Installs dotnet ef tools (ignore error message if already installed)
    3. Applies EF migrations to the current SQL instance
        _I've been unable to reproduce this, but once the EF migration failed to apply when using the bat script. If you see any SQL error, please see Migrations bellow_
```
http://localhost:8000
```

## Description
Containerized application that illustrates my backend capabilities according to the challenge objectives. The app:
- provides basic management of Users - "Login", "Register" (quotes as there is not actual authentication)
- provides basic management of User's Tasks - full CRUD + grouping by stask duration overlap
- uses Domain Driven Design architecture - the core of the application is `VL.Challenge.Domain` project, which contains no references
- uses EF Core with SQL Server (dev edition) running in Docker
- uses Redis as a cache, runnin in Docker
- exposes REST API - `VL.Challenge.API`, ASP.NET Core project that provides API and serves the UI
- UI using Blazor WebAssembly
- Unit tests using `xUnit` and `Moq.AutoMocker`, integrated in the `Dockerfile`
- docker compose orchestration

## Potential improvements
- caching is very limited, more of a PoC than actual solution. It only applies to the following endpoints:
    1. GET task/{id}
    2. PUT task/ - invalidation
    3. DELETE task/ - invalidation
- integration tests - Docker compose provides us with the ability to easily run a copy of our stack and perform integration tests. The easiest would be to have a console application that depends on alternative SQL instance (without volume) consumes `IUsersService` and `ITasksService` and then queries the database for the expected results. This service can then be depended on by `api` and thus `docker compose up` will fail if any unit or integration tests fail. Ii've chosen to omit as I think simply explaining the concept + the contents of this solution are convincing enough
- better calendar/time widgets on the UI - there are free/open source libraries existing for Blazor that provide better functionality, but I've not included any to keep things simple - MudBlazor, SyncFusion, Telerik, etc

## Whys
- Why docker? - obvious.
- Why Blazor - using .Net on the client allows you to reuse object between Server and Client, which I find quite valuable. Also Blazor (Razor pages framework) is quite simpe and intuitive to use.
- Why testing only Domain classes + RedisService?
    1. DDD dicates that the most important layer is the Domain Model, as it represents the Business Model. In such application the Domain entities and objects are responsible of maintaining valid application state. Thus their behavior is critical and they should be tested.
    2. RedisService is simply a bit more interesting case where I can showcase some mocking and testing. The controllers and services are more or less wrappers at this stage. While necessary abstractions in order to maintain good SOLID implementation, there isn't a hudge benefit in testing them

## Unit tests
As mentioned tests are integrated as part of the docker compose process, see `/build/API/Dockerfile`. You can play around with the tests or the source code and see that consecutive `docker compose up -d --build` exeuctions will fail if crucial changes are made.

## Migrations
In order to work the application needs to apply EF migrations to the newly created SQL instance. This happens in `start.bat`, but in case it fails for some reason (Happened once to me, but I was unable to reproduce) just run the following from terminal in the project root:
```
dotnet ef database update --project ../VL.Challenge.Storage -- localhost
``
`-- localhost` provides an argument for the SQL server host. See `ChallengeDbContextFactory` fore implementation.