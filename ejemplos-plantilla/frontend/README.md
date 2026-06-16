# Frontend plantilla Angular 19

Frontend mínimo funcional para tabla genérica.

## Quickstart
```bash
cd frontend
npm install
npm install @eternalcodestudio/primeng-table primeng primeicons @angular/common @angular/core
ng serve
```

Reemplaza los archivos según estructura de este proyecto.

## Estructura
```
frontend/src/
├── app.config.ts
├── app.component.ts
├── services/
│   ├── table-http-adapter.service.ts
│   └── table-notification-adapter.service.ts
└── pages/
    └── employee-table-page/
        ├── employee-table-page.component.ts
        └── employee-table-page.component.html
```

## Adapters obligatorios
- `TableHttpAdapterService` (extiende `ECSPrimengTableHttpService`)
- `TableNotificationAdapterService` (extiende `ECSPrimengTableNotificationService`)

Ambos registrados en providers de `app.config.ts`.

## Test rápido
```bash
ng serve
# Abre http://localhost:4200
# Verifica que conecte a https://localhost:5001 (CORS configurado)
```
