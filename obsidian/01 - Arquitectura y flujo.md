# Arquitectura y flujo

## Arquitectura de alto nivel
```mermaid
flowchart LR
    subgraph A[Angular 19 Frontend]
        UI[ecs-primeng-table]
        HTTP[HttpAdapterService]
        NOTIF[NotificationAdapterService]
    end

    subgraph B[ASP.NET Core 8 API]
        CFG[GET config endpoint]
        DATA[POST data endpoint]
        EXCEL[POST excel endpoint]
        APP[Application Service]
        REPO[Repository]
    end

    subgraph C[SQL Server]
        DB[(Tables + indexes)]
    end

    UI -->|init| CFG
    UI -->|lazy events| DATA
    UI -->|export| EXCEL
    CFG --> APP
    DATA --> APP
    EXCEL --> APP
    APP --> REPO --> DB
```

## Flujo de carga (lazy)
```mermaid
flowchart TD
    A[Usuario interactua tabla] --> B{Evento}
    B -->|Init| C[GET TableConfiguration]
    B -->|Paginar/Filtrar/Ordenar| D[POST TableData with TableQueryRequestModel]
    C --> E[Render columnas y opciones]
    D --> F[ValidateItemsPerPageAndCols]
    F --> G[PerformDynamicQuery]
    G --> H[Sort default o usuario]
    H --> I[Count total sin filtro]
    I --> J[Global filter + column filters]
    J --> K[Count total filtrado]
    K --> L[Pagination clamp]
    L --> M[Select dinamico columnas]
    M --> N[TablePagedResponseModel]
    N --> O[Actualizar UI]
```

## Decisiones clave
- El frontend **no** carga todo el dataset.
- Toda paginación/filtro/orden se ejecuta en SQL vía `IQueryable`.
- El frontend solo envía `TableQueryRequestModel` y pinta respuesta.
- `RowID` (identificador único y estable; Guid recomendado) es obligatorio para features avanzadas.

## Regla de oro
Si hay que elegir entre “lógica en cliente” o “query en base de datos”, para tablas grandes gana base de datos.
