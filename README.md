 # Sentiment Analyser

The application performs sentiment analysis on a text based on previously entered word scores, which can themselves be created, updated or deleted. 

## Technologies

* [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
* [Angular](https://angular.io/)
* [Docker](https://www.docker.com/)
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [Mapster](https://github.com/MapsterMapper/Mapster)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)
* [Serilog](https://serilog.net/)

## Architecture verview

The application architecture is set up using the CQRS pattern and Clean Architecture principles, acting as a sort of a microservice for dealing with sentiments. The application is comprised of two containers, one for the WebAPI and one for the SQL Server database. Additionally, it also contains the ClientApp which is ran separately.

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers.

### Infrastructure

This layer contains classes for accessing external resources such as the database, file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebAPI

This layer is a WebAPI application based on .NET 5. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

### ClientApp

This layer is an Angular application used to provide the client with access to the applications features. This layer depends only on the WebAPI and the endpoints it provides, which can be called using HTTP requests which are only accepted from the ClientApp origin. 

### Tests

This is a separate layer containing the unit and intergration tests for each of the application layers. Only a small portion of the tests have been implemented considering the assignment has a time restriction of course, but a general overview of the test concept is present.

## Setting up the environment

A prerequisite for running the WebAPI is having [Docker Desktop](https://www.docker.com/products/docker-desktop) installed and running. The backend can be started in two ways. By entering the following commands in the project root folder:

```
docker-compose build
docker-compose up -d
```
which build and start up the WebAPI and the SQL server containers in detached mode. This can take some time until the containers are completely ready (30-60 seconds). The alternative is to run the API in debug mode by entering visual studio, selecting the docker-compose project as the start-up project, and runnig the application. 

There is a possibility that the container creation will fail when running for the first time (login failed for user 'sa') which is a knows sql server issue, because the database server is started up but not ready to receive connections yet. It this case just start the app again and everything will work fine.

The swagger page of the WebAPI can be found on the following link (which is https only) https://localhost:5006/swagger/index.html which can be used for testing and documentation. The SQL Server database is started on port 21433, with username: sa; password: Finbet123!, with the initial sentiments added.

A prerequisite for running the ClientApp is having [Angular](https://angular.io/guide/setup-local) 11 installed. The app can be started by running the following command in the SentimentAnalyser.ClientApp folder:

```
npm start
```
The app can then be found on http://localhost:4200
