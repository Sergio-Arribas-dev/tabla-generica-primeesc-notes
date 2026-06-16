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
**Estrategia:**
- **URL:** `/api/v1/table/{resource}/configuration` y `/api/v1/table/{resource}/data`
- **Si cambias:** semántica de filtros, añade `v2` (ej. `/v2/table/...`)
- **Backward:** Mantener `v1` funcionando 1-2 ciclos de desarrollo.
- **Deprecación:** Comunicar en logs/headers (`Deprecation: true`).

**Ejemplo cambio sin romper:**
- Añade campo opcional a DTO
- Frontend envía ambos (viejo + nuevo)
- Backend soporta ambos
- Migrar cuando clientes actualizados
