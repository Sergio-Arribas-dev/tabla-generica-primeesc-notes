# Backend plantilla .NET 8

Backend mínimo funcional para tabla genérica.

## Quickstart
```bash
cd backend
# Crear proyecto
dotnet new webapi -n Demo.Tables --framework net8.0
cd Demo.Tables

# Instalar paquetes requeridos
dotnet add package ECS.PrimeNGTable
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
dotnet add package System.Linq.Dynamic.Core --version 1.7.0
dotnet add package ClosedXML --version 0.105.0
```

**Luego:** Reemplaza archivos `.cs` de esta carpeta en tu proyecto (respeta estructura de namespaces).

## Estructura
```
backend/
├── Program.cs                 (configuración DI)
├── appsettings.Development.json
├── Controllers/
│   └── EmployeeTableController.cs
├── Services/
│   ├── IEmployeeTableService.cs
│   └── EmployeeTableService.cs
├── Repositories/
│   ├── IEmployeeTableRepository.cs
│   └── EmployeeTableRepository.cs
├── Data/
│   ├── AppDbContext.cs
│   └── DbContextFactory.cs
├── DTOs/
│   └── EmployeeTableDto.cs
└── Entities/
    └── Employee.cs
```

## Base de datos
Ejecuta SQL seeding (`../sql/seed.sql`) después de crear tablas.

## Test rápido
```bash
dotnet run
# Abre https://localhost:5001/swagger
# Prueba GET /api/employee-table/configuration
```
