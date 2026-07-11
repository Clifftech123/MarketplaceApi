# MarketplaceApi

MarketplaceApi is a Web API for an online marketplace, built on .NET 10.

It lets store owners list products under categories, tracks stock levels, and lets customers build a shopping cart and turn it into an order. Orders move through a status lifecycle from pending through paid, shipped and delivered, or can be cancelled. Payments for an order are recorded against Stripe transaction data, including amount, currency, status and method. The system also tracks notifications and email logs sent to users, and manages user accounts and roles through ASP.NET Core Identity.

## Technology Stack

* .NET 10
* ASP.NET Core Web API
* Entity Framework Core 10 with SQL Server
* ASP.NET Core Identity for user accounts and roles

## Configuration

Connection strings and other sensitive values are kept out of `appsettings.json` and loaded through .NET user secrets. Set the `SqlServerOptions:ConnectionString` value using the .NET user secrets tool for this project before running it against a local database.

## Running the API

Restore and build the project, then run it from the project root.

```
dotnet restore
dotnet build
dotnet run
```

In development, OpenAPI metadata is exposed automatically once the application starts.

## Current Status

The domain model, persistence layer, and the data transfer objects and mappings for every aggregate are in place. Controllers, application services, authentication endpoints and domain event publishing have not been implemented yet.
