# CraveCart

This project is a web application which loads data from San Francisco's food truck open dataset and provides GUI for finding closest food trucks to the user's location based on the users preferred food.
Standalone application which loads all san francisco food trucks upon startup.

## Projects

Application is divided into multiple projects with clean architecture and DDD principles given into account.
As the application functionality is rather small and further division would be unnecessary, the libraries count is smaller than in enterprise sized projects.
One of the main benefits of this architecture is that it is easy to extend and maintain. It is also easy to test as the business logic is separated from the infrastructure.
All following project are .NET 8 projects, linked to each other. Main standalone applications are in bold.

- **CraveCart.Web** - Main web application
- CraveCart.Application - Main application / business logic layer
- CraveCart.Domain - Domain layer including domain models and core interfaces
- CraveCart.Infrastructure - Library implementing all required infrastructure

## Tool Set

Application was written using:

- .NET 8 SDK
- Visual Studio 2022

## Libraries and technologies

- Blazor adaptive
- Geolocation (https://github.com/scottschluer/geolocation)

## How to run
- Host the application locally or debug using visual studio
- BaseUrl for San Francisco food trucks API can be changed in appsettings.json
- Application logs to console - inspect console logs in case of unexpected application errors