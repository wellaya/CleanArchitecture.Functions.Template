# Clean Architecture — Azure Functions Template

A `dotnet new` template that brings **Clean Architecture** to **Azure Functions** (.NET 10, isolated worker), consuming shared cross-cutting code from [`Platform.Shared`](https://github.com/wellaya/Platform.Shared).

## Installation

```bash
dotnet new install Clean.Architecture.Functions.Template::*
```

## Create a New Project

```bash
dotnet new ca-func -n Orders -o Orders
```

## Layer Boundaries

| Project                  | May Reference EF Core?                                        |
| ------------------------ | -------------------------------------------------------------- |
| `{Name}.Domain`          | ❌ No                                                          |
| `{Name}.Application`     | ✅ Yes — Defines `IApplicationDbContext` and executes queries. |
| `Platform.Application`   | ❌ No — Remains persistence-agnostic.                          |
| `{Name}.Infrastructure`  | ✅ Yes — Contains the EF provider and migrations.              |

For the architectural rationale behind this separation, see [ADR 0002](docs/decisions/0002-ef-core-boundary.md).