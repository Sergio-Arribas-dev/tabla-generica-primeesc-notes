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

## Compatibilidad Angular 19
- Revisar versión de PrimeNG soportada por Angular 19.
- Si usas paquete compilado de otra versión, validar:
  - build
  - estilos
  - eventos
  - tipos
