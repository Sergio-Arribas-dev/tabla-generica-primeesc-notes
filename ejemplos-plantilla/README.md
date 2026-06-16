# Plantilla ejecutable — Backend + Frontend

Esta carpeta contiene ejemplos funcionales listos para copiar/pegar y ejecutar.

## Estructura

```
ejemplos-plantilla/
├── backend/              → .NET 8 con table genérica
├── frontend/             → Angular 19 con tabla
└── sql/                  → Scripts de BD de ejemplo
```

## Cómo tirar rápido

### 1) Backend
```bash
cd backend
dotnet new webapi -n Demo.Tables --framework net8.0
# Añade paquetes: ECS.PrimeNGTable, EntityFrameworkCore.SqlServer, ClosedXML, etc.
# Copia los archivos .cs de esta carpeta
dotnet run
# Swagger en https://localhost:5001
```

### 2) Frontend
```bash
cd frontend
npm install
ng serve
# http://localhost:4200
```

### 3) Base de datos
- SQL Server: ejecuta `sql/seed.sql`
- Actualiza connection string en `appsettings.Development.json`

## Verificación rápida
1. Backend arrancado: curl https://localhost:5001/swagger
2. Frontend arrancado: http://localhost:4200
3. Tabla visible y conecta al backend
4. Prueba filtros/paginación
