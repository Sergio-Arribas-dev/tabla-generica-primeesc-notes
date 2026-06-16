# Contratos API y modelos

## Request principal (`TableQueryRequestModel`)
Campos clave:
- `page`, `pageSize`
- `columns`
- `sort[]` (`field`, `order`)
- `filter{}` por columna
- `globalFilter`
- `dateFormat`, `dateTimezone`, `dateCulture`, `exportDateFormat`

## Response principal (`TablePagedResponseModel`)
- `page`
- `totalRecords`
- `totalRecordsNotFiltered`
- `data[]`

## Configuración (`TableConfigurationModel`)
- `allowedItemsPerPage`
- `columnsInfo[]`
- `dateFormat`, `dateTimezone`, `dateCulture`, `exportDateFormat`
- `maxViews`

## Modelos de columna más importantes
- `ColumnMetadataModel`
- `ColumnMetadataOverrideModel`
- `ColumnFilterModel`
- `ColumnSortModel`

## Regla de consistencia
Los nombres de propiedades del DTO backend deben mapear de forma estable con `field` en frontend. Evita renombrar sin estrategia de versionado.

## Versionado de contrato
Recomendación:
- `/api/v1/table/...`
- Si cambias semántica de filtros, crear `v2`.
- Mantener compatibilidad backward en una ventana temporal.
