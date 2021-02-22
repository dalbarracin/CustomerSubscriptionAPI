# CustomerSubscriptionAPI
ASP.Net WebAPI application for managing CRUD operations about customers subscriptions and master data

## Web MVC application
This code sample presents a Web API application based on Net Core 3.1 as a backend project for managin Customers and Products Master data as well as Subscriptions (Customers being register with multiples Products).

Can be downloaded as a single container on Docker hub (https://hub.docker.com/repository/docker/dalbarracin/customersubscriptionapi).

## Architectural constraints

Some architectural decisions were made by creating a repository implementations (Customer, Products and Subscriptions) by applying Repository pattern whose definitions are configured by using Dependency Injection pattern as well as Strategy pattern and SOLID Principles (Liskov substitution).
Each repository implementation initialize data in memory, using Collections, and persisting data will make use of those collections. These implementations can be considered as a Data access layer, and can be extended for others data access implementations (EF 6, Non relational database, etc).
Also, ApiContoller are defined and some repositories usages are declared on those clases.

## Requirements

- Docker installed
- Docker compose configured

## Running the Application

1. Download source code from Github

2. Open command prompt and locate to root source code folder where "docker-compose.yml" is located to.

3. Execute `docker-compose up --build`

4. Open the web application "http://localhost:44387/"
