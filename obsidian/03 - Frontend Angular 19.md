# Frontend Angular 19

## Objetivo
Mantener el front **delgado**: configura la tabla, escucha eventos y delega todo lo costoso al backend.

## Mínimo funcional
- Importar componente de tabla.
- Crear `tableOptions` con `createTableOptions(...)`.
- Definir:
  - `urlTableConfiguration`
  - `urlTableData`

## Servicios obligatorios (abstracciones)
Implementar:
- `ECSPrimengTableHttpService`
- `ECSPrimengTableNotificationService`

Registrar en providers para DIP real.

## Configuración recomendada
- `globalFilter.maxLength`: corto (20-50).
- `verticalScroll.fitToContainer`: `true` para UX estable.
- `clearFiltersEnabled` y `clearSortsEnabled`: `true`.
- `selectorEnabled`: `true` salvo requerimiento estricto.

## Eventos
Usar `onDataEndUpdate` para:
- Sincronizar estado externo.
- Métricas de uso.
- Resolver post-procesos visuales.

## KISS en Angular
- Evita componer lógica de filtros en cliente.
- No dupliques modelo de backend en múltiples sitios.
- Un `Facade` por pantalla de tabla si hay lógica extra.

## Angular 19 espec\u00edfico
- `package.json`: Angular \u226519.0.0, TypeScript \u22655.6.0
- PrimeNG \u226519.0.0 (compatible con Angular 19)
- Usar `standalone` en componentes (no m\u00f3dulos)
- `app.config.ts` para providers (no `AppModule`)
- `createTableOptions()` obligatorio para inicializar tabla
- Validar imports correctos en cada servicio/component
