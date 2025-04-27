# SupplementsShop
> A minimal e-commerce app for nutritional supplements built with ASP.NET Core 8.
> One-command local run with Docker Compose, automated tests via xUnit, and CI on GitHub Actions.

## Overview
This project implements a clean, layered ASP.NET Core backend with real-time inventory validation via an external API:

- Customers browse products, add to cart and place orders;
- Each checkout calls an external **InventoryService API** (https://github.com/diomedes-vba/iWarehouse) to confirm stock before finalising (will still work when API is not accessible)
- Session cart (guests) seamlessly merges with DB cart (logged-in users)

## The purpose of the project
The project was built as a hands-on sandbox to practice clean ASP.NET Core patterns. 
* Practice implementing architectures such as **MVC** and **Onion**
* Work with basic database persistence and **Entity Framework**
* Implement user-based authorization with **ASP.NET Core Identity**
* Practice working with external APIs
* Complete unit testing with **xUnit**
* Containerise both the app and database with **Docker**

## Tech Stack

| Layer | Tech | Notes                                     |
|-------|------|-------------------------------------------|
| **Runtime** | ASP.NET Core 8 MVC | Razor views, dependency-injection         |
| **Data** | SQL Server 2022 | Code-first migrations with EF Core        |
| **Auth** | ASP.NET Core Identity | Role-based (User / Admin)                 |
| **Testing** | xUnit | Unit tests for different layers           |
| **CI/CD** | GitHub Actions | Build → test  on every push |
| **Containers** | Docker & Docker Compose | `web` + `db` services                     |
| **Dev tools** | Git, SSMS |                                           |

## Data Flow and Architecture
> Onion architecture and layers keep core business logic isolated

4 layers: Domain, Infrastructure, Application and Web.
```mermaid
graph LR;
    UI[Web (MVC Views)] --> App[Application Layer]
    App --> Domain
    App --> Infra[Infrastructure]
    Infra --> DB[(SQL Server)]
    App --> InventoryAPI[/Inventory REST API/]
```

## Features
* Role-based authentication (User / Admin) with Identity.
* Hybrid Cart – session storage for guests, DB persistence for users; auto-merge on sign-in.
* Inventory Check – JWT-secured call to InventoryService prevents oversells.
* Server-side pagination (PagedList<T>) keeps product queries lightweight and lessens the load on database.
* Admin portal for product management and image upload.

## Running the project (Docker Compose)
git clone https://github.com/diomedes-vba/SupplementsShop.git
cd SupplementsShop

docker compose up --build

## Tests & CI
dotnet test