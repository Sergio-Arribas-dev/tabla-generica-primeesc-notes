# Plantilla nueva tabla en 20 minutos

> Objetivo: crear una nueva tabla (Customers, Products, Invoices, etc.) reutilizando el patron sin improvisar.

## 0) Convenciones base (obligatorio)

- Mantener nombre de fila como `RowID` en backend y `rowID` en frontend.
- `RowID` debe ser unico y estable (Guid recomendado; int valido si es estable).
- Un endpoint para configuracion y otro para datos.
- El frontend no filtra en memoria: delega al backend.

## 1) Checklist rapido (orden recomendado)

- [ ] Crear carpeta de tabla en backend.
- [ ] Crear DTO con `ColumnAttributes`.
- [ ] Crear repository + service + controller.
- [ ] Registrar DI en `Program.cs`.
- [ ] Crear pagina/componente frontend.
- [ ] Configurar `createTableOptions` con endpoints.
- [ ] Probar GET configuracion + POST data en Swagger.
- [ ] Probar filtros/sort/paginacion en UI.
- [ ] Validar indices SQL de columnas criticas.

## 2) Estructura de archivos por nueva tabla

```text
backend/
  Tables/
    <EntityPlural>/
      <Entity>TableDto.cs
      I<Entity>TableRepository.cs
      <Entity>TableRepository.cs
      I<Entity>TableService.cs
      <Entity>TableService.cs
      <Entity>TableController.cs

frontend/src/
  pages/
    <entity>-table-page/
      <entity>-table-page.component.ts
      <entity>-table-page.component.html
```

Reutilizable una sola vez para toda la app:
- `table-http-adapter.service.ts`
- `table-notification-adapter.service.ts`
- Providers globales (`app.config.ts` o `app.module.ts`)

## 3) Plantilla backend (copy-paste)

## 3.1 DTO

```csharp
using ECS.PrimengTable.Attributes;
using ECS.PrimengTable.Enums;

namespace Demo.Tables.<EntityPlural>;

public sealed class <Entity>TableDto
{
    [ColumnAttributes(sendColumnAttributes: false)]
    public int RowID { get; set; } // o Guid

    [ColumnAttributes("Nombre", canBeGlobalFiltered: true)]
    public string Name { get; set; } = string.Empty;

    [ColumnAttributes("Estado", filterPredefinedValuesName: "statusList")]
    public string Status { get; set; } = string.Empty;

    [ColumnAttributes("Fecha", dataType: DataType.Date)]
    public DateTime? CreatedAt { get; set; }

    [ColumnAttributes("Activo", dataType: DataType.Boolean)]
    public bool IsActive { get; set; }
}
```

## 3.2 Repository

```csharp
namespace Demo.Tables.<EntityPlural>;

public interface I<Entity>TableRepository
{
    IQueryable<<Entity>> GetQueryable();
}

public sealed class <Entity>TableRepository : I<Entity>TableRepository
{
    private readonly AppDbContext _db;

    public <Entity>TableRepository(AppDbContext db)
    {
        _db = db;
    }

    public IQueryable<<Entity>> GetQueryable()
    {
        return _db.Set<<Entity>>().AsNoTracking();
    }
}
```

## 3.3 Service

```csharp
using ECS.PrimengTable.Enums;
using ECS.PrimengTable.Models;
using ECS.PrimengTable.Services;

namespace Demo.Tables.<EntityPlural>;

public interface I<Entity>TableService
{
    TableConfigurationModel GetTableConfiguration(string culture, string timezone);
    (bool success, string? error, TablePagedResponseModel? data) GetTableData(TableQueryRequestModel request);
}

public sealed class <Entity>TableService : I<Entity>TableService
{
    private static readonly int[] AllowedPageSizes = [10, 25, 50, 100];

    private readonly I<Entity>TableRepository _repository;

    public <Entity>TableService(I<Entity>TableRepository repository)
    {
        _repository = repository;
    }

    public TableConfigurationModel GetTableConfiguration(string culture, string timezone)
    {
        return EcsPrimengTableService.GetTableConfiguration<<Entity>TableDto>(
            allowedItemsPerPage: AllowedPageSizes,
            dateFormat: "dd/MM/yyyy HH:mm",
            dateTimezone: timezone,
            dateCulture: culture,
            exportDateFormat: "dd/mm/yyyy hh:mm"
        );
    }

    public (bool success, string? error, TablePagedResponseModel? data) GetTableData(TableQueryRequestModel request)
    {
        if (!EcsPrimengTableService.ValidateItemsPerPageAndCols(request.PageSize, request.Columns?.ToList(), AllowedPageSizes))
            return (false, "PageSize o columnas invalidas", null);

        var result = EcsPrimengTableService.PerformDynamicQuery(
            inputData: request,
            baseQuery: BuildBaseQuery(),
            defaultSortColumnName: [nameof(<Entity>TableDto.Name)],
            defaultSortOrder: [ColumnSort.Ascending]
        );

        return (true, null, result);
    }

    private IQueryable<<Entity>TableDto> BuildBaseQuery()
    {
        return _repository.GetQueryable().Select(x => new <Entity>TableDto
        {
            RowID = x.Id, // mapear PK real
            Name = x.Name,
            Status = x.Status,
            CreatedAt = x.CreatedAt,
            IsActive = x.IsActive
        });
    }
}
```

## 3.4 Controller

```csharp
using ECS.PrimengTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Tables.<EntityPlural>;

[ApiController]
[Route("api/<entity>-table")]
public sealed class <Entity>TableController : ControllerBase
{
    private readonly I<Entity>TableService _service;

    public <Entity>TableController(I<Entity>TableService service)
    {
        _service = service;
    }

    [HttpGet("configuration")]
    public ActionResult<TableConfigurationModel> GetConfiguration()
    {
        return Ok(_service.GetTableConfiguration("es-ES", "+01:00"));
    }

    [HttpPost("data")]
    public IActionResult GetData([FromBody] TableQueryRequestModel request)
    {
        var result = _service.GetTableData(request);
        if (!result.success) return BadRequest(new { message = result.error });
        return Ok(result.data);
    }
}
```

## 3.5 DI en Program

```csharp
builder.Services.AddScoped<I<Entity>TableRepository, <Entity>TableRepository>();
builder.Services.AddScoped<I<Entity>TableService, <Entity>TableService>();
```

## 4) Plantilla frontend Angular 19 (standalone)

```ts
import { Component } from '@angular/core';
import {
  ECSPrimengTable,
  ITableOptions,
  createTableOptions,
  IPredefinedFilter,
  ITableButton
} from '@eternalcodestudio/primeng-table';

@Component({
  selector: 'app-<entity>-table-page',
  standalone: true,
  imports: [ECSPrimengTable],
  template: `<ecs-primeng-table [tableOptions]="tableOptions"></ecs-primeng-table>`
})
export class <Entity>TablePageComponent {
  readonly predefined: { [key: string]: IPredefinedFilter[] } = {
    statusList: [
      { value: 'ACTIVE', name: 'Activo', displayTag: true, icon: 'pi pi-check', iconColor: '#16a34a' },
      { value: 'INACTIVE', name: 'Inactivo', displayTag: true, icon: 'pi pi-times', iconColor: '#dc2626' }
    ]
  };

  readonly rowButtons: ITableButton[] = [
    {
      icon: 'pi pi-search',
      tooltip: 'Detalle',
      action: (rowData) => this.openDetail(rowData.rowID)
    }
  ];

  tableOptions: ITableOptions = createTableOptions({
    urlTableConfiguration: '<entity>-table/configuration',
    urlTableData: '<entity>-table/data',
    predefinedFilters: this.predefined,
    header: {
      clearFiltersEnabled: true,
      clearSortsEnabled: true
    },
    rows: {
      action: {
        header: 'Acciones',
        frozen: true,
        width: 140,
        buttons: this.rowButtons
      }
    },
    globalFilter: {
      enabled: true,
      maxLength: 30
    }
  });

  private openDetail(rowID: string | number): void {
    console.log('Detalle', rowID);
  }
}
```

## 5) Plantilla frontend Angular 14 (AppModule)

## 5.1 Modulo

```ts
@NgModule({
  declarations: [AppComponent, <Entity>TablePageComponent],
  imports: [BrowserModule, HttpClientModule, ECSPrimengTable],
  providers: [
    MessageService,
    { provide: ECSPrimengTableHttpService, useClass: TableHttpAdapterService },
    { provide: ECSPrimengTableNotificationService, useClass: TableNotificationAdapterService }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
```

## 5.2 Componente

- Igual al de Angular 19, pero quitando `standalone: true` y pasandolo a `declarations` del modulo.

## 6) Validacion minima antes de cerrar tarea

- Swagger:
  - GET `api/<entity>-table/configuration` responde 200.
  - POST `api/<entity>-table/data` responde 200 con `totalRecords` y `data`.
- UI:
  - Tabla renderiza.
  - Filtro global funciona.
  - Orden funciona.
  - Boton por fila recibe `rowID` correcto.
- Rendimiento:
  - `pageSize` limitado.
  - Indice en columnas de filtro/sort.

## 7) Errores frecuentes (y solucion)

- Error: fila no se selecciona bien.
  - Causa: `RowID` no unico o cambia entre requests.
  - Solucion: exponer ID estable y no derivado de posicion.

- Error: filtros no aplican.
  - Causa: nombre de columna en backend no coincide con `field` del request.
  - Solucion: revisar mapeo y convencion de nombres.

- Error: predefined no pinta icono/imagen.
  - Causa: `value` no coincide con dato backend.
  - Solucion: alinear valores exactos (case-sensitive recomendado).

## 8) Tiempo estimado realista

- Primera tabla en proyecto nuevo: 1.5h - 3h.
- Nueva tabla en proyecto ya armado: 20 - 45 min.

Depende de:
- Complejidad de columnas y filtros.
- Si hay botones/acciones por fila.
- Si hay reglas de seguridad por rol.
