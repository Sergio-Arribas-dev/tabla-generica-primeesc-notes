# Plan de implementación y checklist

## Fase 1 — Backend mínimo
- [ ] Crear DTO de tabla con `RowID`.
- [ ] Decorar columnas con metadatos.
- [ ] Implementar endpoint `GetTableConfiguration`.
- [ ] Implementar endpoint `GetTableData`.
- [ ] Validar `itemsPerPage` y columnas.
- [ ] Añadir `AsNoTracking` e índices SQL.

## Fase 2 — Frontend mínimo
- [ ] Instalar dependencias compatibles Angular 19.
- [ ] Implementar adaptador HTTP.
- [ ] Implementar adaptador de notificaciones.
- [ ] Configurar `createTableOptions`.
- [ ] Pintar `<ecs-primeng-table>`.

## Fase 3 — Funcionalidades avanzadas
- [ ] Predefined filters para catálogos pequeños.
- [ ] Vistas persistidas (session/local/db).
- [ ] Export Excel.
- [ ] Botones de fila y cabecera.

## Fase 4 — Calidad
- [ ] Tests unitarios de servicio.
- [ ] Tests integración endpoint + DB.
- [ ] Prueba de carga con dataset realista.
- [ ] Revisión de tiempos p95 y p99.

## DoD (Definition of Done)
- [ ] Paginación y filtros correctos.
- [ ] Sin timeouts en casos nominales.
- [ ] Contratos documentados.
- [ ] Observabilidad activa.
- [ ] Guía operativa actualizada.
