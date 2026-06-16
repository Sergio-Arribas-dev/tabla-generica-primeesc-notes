# Backend plantilla .NET 8

Backend mínimo funcional para tabla genérica.

## Quickstart
```bash
cd backend
dotnet new globaljson --sdk-version 8.0.0 --roll-forward latestFeature
dotnet new webapi -n Demo.Tables
cd Demo.Tables
dotnet add package ECS.PrimeNGTable
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package System.Linq.Dynamic.Core
dotnet add package ClosedXML
```

Reemplaza los archivos según estructura `/src` de este proyecto.

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
