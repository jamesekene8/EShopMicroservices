# EShop Microservices (.NET 8)

An e-commerce back end split into independently deployable **.NET 8** microservices. Each service owns its own data store. They are built with **Vertical Slice Architecture + CQRS**, share cross-cutting concerns through a building-blocks library, and run together with **Docker Compose**.

![.NET](https://img.shields.io/badge/.NET_8-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-DC382D?logo=redis&logoColor=white)
![gRPC](https://img.shields.io/badge/gRPC-244c5a?logo=grpc&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=white)
![Status](https://img.shields.io/badge/status-in_progress-orange)

## Services

| Service | Responsibility | Data | API style | Local port |
|---|---|---|---|---|
| **Catalog.API** | Products CRUD, filter by category | PostgreSQL via **Marten** (document DB) | REST, minimal APIs with **Carter** | `6000` |
| **Basket.API** | Shopping cart per user | PostgreSQL via Marten, **Redis** cache | REST (Carter) | `6001` |
| **Discount.Grpc** | Coupons per product | SQLite via EF Core | **gRPC** | – |
| **BuildingBlocks** | Shared library: CQRS abstractions (`ICommand`/`IQuery`), MediatR pipeline behaviours for validation and logging, global exception handler | – | – | – |

### Design highlights

- **Vertical slices:** each feature (e.g. `CreateProduct`, `GetBasket`) keeps its endpoint, command/query, handler and validator together.
- **MediatR pipeline behaviours:** `ValidationBehavior` (FluentValidation) and `LoggingBehavior` run around every handler.
- **Decorator-based caching:** `CachedBasketRepository` wraps the basket repository with Redis using Scrutor's `Decorate`, so the handler code doesn't change.
- **Health checks** at `/health` for PostgreSQL and Redis, in the HealthChecks UI format.
- **Mapster** for object mapping and **ProblemDetails** for consistent error responses.

## Endpoints

```
Catalog   GET    /products            GET /products/{id}     GET /products/category/{category}
          POST   /products            PUT /products          DELETE /products/{id}
Basket    GET    /basket/{userName}   POST /basket           DELETE /basket/{userName}
Discount  gRPC   GetDiscount · CreateDiscount · UpdateDiscount · DeleteDiscount
```

## Running locally

**Prerequisites:** Docker Desktop. The .NET 8 SDK is only needed if you run services outside Docker.

```bash
cd src
docker compose -f docker-compose.yml -f docker-compose.override.yml up -d --build
```

- Catalog: http://localhost:6000/products. Sample products are seeded in development.
- Basket: http://localhost:6001/basket/{userName}
- The databases (Postgres on 5432/5433, Redis on 6379) use local development credentials defined in the compose override file.

## Roadmap

- [x] Catalog service with seeding, validation, paging and health checks
- [x] Basket service with Redis caching
- [x] Discount gRPC service (SQLite)
- [ ] Basket → Discount gRPC call to apply coupons at checkout
- [ ] Add Discount to Docker Compose
- [ ] Ordering service (DDD + Clean Architecture, SQL Server)
- [ ] Async messaging with RabbitMQ + MassTransit (basket checkout event)
- [ ] YARP API gateway and a web client

## Acknowledgements

Built while following a .NET 8 microservices course, as hands-on practice with distributed-system patterns.

## Author

**James Uguna**: [GitHub](https://github.com/jamesekene8)
