# Ejemplo end-to-end backend (.NET 8)

> Mismo caso en todos los apuntes: **tabla de empleados**.
> Campos: `RowID`, `Username`, `Salary`, `BirthDate`, `EmploymentStatus`, `HasHouse`, `StatusColorHex`.

## 1) DTO de tabla
```csharp
using ECS.PrimengTable.Attributes;
using ECS.PrimengTable.Enums;

namespace Demo.Tables.Employees;

public sealed class EmployeeTableDto
{
    [ColumnAttributes(sendColumnAttributes: false)]
    public Guid RowID { get; set; }

    [ColumnAttributes("Usuario", dataType: DataType.Text, canBeGlobalFiltered: true)]
    public string Username { get; set; } = string.Empty;

    [ColumnAttributes("Salario", dataType: DataType.Numeric, dataAlignHorizontal: DataAlignHorizontal.Right)]
    public decimal Salary { get; set; }

    [ColumnAttributes("Fecha nacimiento", dataType: DataType.Date, dateFormat: "dd/MM/yyyy", exportDateFormat: "dd/mm/yyyy")]
    public DateTime? BirthDate { get; set; }

    [ColumnAttributes("Estado", dataType: DataType.Text, filterPredefinedValuesName: "employmentStatus")]
    public string EmploymentStatus { get; set; } = string.Empty;

    [ColumnAttributes("Casa", dataType: DataType.Boolean)]
    public bool HasHouse { get; set; }

    [ColumnAttributes(sendColumnAttributes: false)]
    public string StatusColorHex { get; set; } = "#999999";
}
```

## 2) Entity EF Core (ejemplo)
```csharp
namespace Demo.Infrastructure.Entities;

public sealed class Employee
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public DateTime? BirthDate { get; set; }
    public string EmploymentStatus { get; set; } = string.Empty;
    public bool HasHouse { get; set; }
    public string StatusColorHex { get; set; } = "#999999";
    public DateTime CreatedUtc { get; set; }
}
```

## 3) Contrato de repositorio
```csharp
using Demo.Infrastructure.Entities;

namespace Demo.Tables.Employees;

public interface IEmployeeTableRepository
{
    IQueryable<Employee> GetQueryable();
}
```

## 4) Implementación de repositorio
```csharp
using Demo.Infrastructure;
using Demo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Tables.Employees;

public sealed class EmployeeTableRepository : IEmployeeTableRepository
{
    private readonly AppDbContext _db;

    public EmployeeTableRepository(AppDbContext db)
    {
        _db = db;
    }

    public IQueryable<Employee> GetQueryable()
    {
        return _db.Employees.AsNoTracking();
    }
}
```

## 5) Contrato de servicio
```csharp
using ECS.PrimengTable.Models;

namespace Demo.Tables.Employees;

public interface IEmployeeTableService
{
    TableConfigurationModel GetTableConfiguration(string userCulture, string userTimezone);
    (bool success, string? error, TablePagedResponseModel? data) GetTableData(TableQueryRequestModel request);
}
```

## 6) Implementación de servicio
```csharp
using ECS.PrimengTable.Enums;
using ECS.PrimengTable.Models;
using ECS.PrimengTable.Services;

namespace Demo.Tables.Employees;

public sealed class EmployeeTableService : IEmployeeTableService
{
    private static readonly int[] AllowedPageSizes = [10, 25, 50, 100];

    private static readonly List<string> DefaultSortColumns = [nameof(EmployeeTableDto.Username)];
    private static readonly List<ColumnSort> DefaultSortOrder = [ColumnSort.Ascending];

    private readonly IEmployeeTableRepository _repository;

    public EmployeeTableService(IEmployeeTableRepository repository)
    {
        _repository = repository;
    }

    public TableConfigurationModel GetTableConfiguration(string userCulture, string userTimezone)
    {
        return EcsPrimengTableService.GetTableConfiguration<EmployeeTableDto>(
            allowedItemsPerPage: AllowedPageSizes,
            dateFormat: "dd/MM/yyyy HH:mm",
            dateTimezone: userTimezone,
            dateCulture: userCulture,
            exportDateFormat: "dd/mm/yyyy hh:mm",
            maxViews: 10
        );
    }

    public (bool success, string? error, TablePagedResponseModel? data) GetTableData(TableQueryRequestModel request)
    {
        if (!EcsPrimengTableService.ValidateItemsPerPageAndCols(request.PageSize, request.Columns?.ToList(), AllowedPageSizes))
        {
            return (false, "PageSize o columnas inválidas", null);
        }

        var baseQuery = BuildBaseQuery();

        var result = EcsPrimengTableService.PerformDynamicQuery(
            inputData: request,
            baseQuery: baseQuery,
            defaultSortColumnName: DefaultSortColumns,
            defaultSortOrder: DefaultSortOrder
        );

        return (true, null, result);
    }

    private IQueryable<EmployeeTableDto> BuildBaseQuery()
    {
        return _repository.GetQueryable().Select(x => new EmployeeTableDto
        {
            RowID = x.Id,
            Username = x.Username,
            Salary = x.Salary,
            BirthDate = x.BirthDate,
            EmploymentStatus = x.EmploymentStatus,
            HasHouse = x.HasHouse,
            StatusColorHex = x.StatusColorHex
        });
    }
}
```

## 7) Controller
```csharp
using ECS.PrimengTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Tables.Employees;

[ApiController]
[Route("api/employee-table")]
public sealed class EmployeeTableController : ControllerBase
{
    private readonly IEmployeeTableService _service;

    public EmployeeTableController(IEmployeeTableService service)
    {
        _service = service;
    }

    [HttpGet("configuration")]
    public ActionResult<TableConfigurationModel> GetConfiguration()
    {
        // Ejemplo: en real, obtener de perfil/claims
        var culture = "es-ES";
        var timezone = "+01:00";

        return Ok(_service.GetTableConfiguration(culture, timezone));
    }

    [HttpPost("data")]
    public IActionResult GetData([FromBody] TableQueryRequestModel request)
    {
        var result = _service.GetTableData(request);

        if (!result.success)
            return BadRequest(new { message = result.error });

        return Ok(result.data);
    }
}
```

## 8) Registro DI (Program.cs)
```csharp
using Demo.Tables.Employees;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeTableRepository, EmployeeTableRepository>();
builder.Services.AddScoped<IEmployeeTableService, EmployeeTableService>();

var app = builder.Build();
app.MapControllers();
app.Run();
```

## 9) Índices SQL mínimos sugeridos
```sql
CREATE INDEX IX_Employees_Username ON dbo.Employees(Username);
CREATE INDEX IX_Employees_EmploymentStatus ON dbo.Employees(EmploymentStatus);
CREATE INDEX IX_Employees_BirthDate ON dbo.Employees(BirthDate);
CREATE INDEX IX_Employees_HasHouse ON dbo.Employees(HasHouse);
```

## 10) Checklist backend rápido
- `RowID` existe y es único.
- `AsNoTracking` aplicado.
- Validación de `PageSize` y columnas antes de query dinámica.
- `BuildBaseQuery()` reutilizable para tabla y export.
- Índices en columnas de filtro/sort.
