# Operación, rendimiento y troubleshooting

## Errores típicos
1. Tabla no renderiza:
   - Falta `createTableOptions`.
   - Endpoint de configuración incorrecto.
2. Filtros fallan:
   - Tipo DTO no coincide (`nullable` mal definido).
   - `MatchMode` no soportado.
3. Lentitud:
   - Falta índice en columnas de filtro/sort.
   - `PageSize` demasiado alto.
4. Selección fila inestable:
   - `RowID` no único o no enviado.

## Observabilidad mínima
- Log por request con:
  - endpoint
  - ms total
  - filas devueltas
  - page/pageSize
- Métricas:
  - p95 de `GetTableData`
  - ratio de errores por `matchMode`
  - uso de exportación

## Reglas de rendimiento
- Nunca materializar temprano (`ToList`) antes de aplicar filtros.
- Aplicar `select` dinámico al final.
- Limitar columnas solicitables.
- Reducir payload en columnas no visibles cuando proceda.

## Guía rápida de diagnóstico
1. Reproducir con mismo payload en Swagger.
2. Revisar query SQL generada.
3. Revisar plan de ejecución.
4. Confirmar índices.
5. Comparar `totalRecords` vs `totalRecordsNotFiltered`.
