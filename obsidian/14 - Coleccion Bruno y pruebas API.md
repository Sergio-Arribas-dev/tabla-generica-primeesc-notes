# Coleccion Bruno y pruebas API

## 1) Ubicacion

Coleccion lista para importar/abrir en Bruno:
- `bruno/TablaGenericaPrimeEsc/`

## 2) Requests incluidos

### Employee
- `01 - Get Configuration.bru`
- `02 - Post Data Initial.bru`
- `03 - Post Data Filters And Sort.bru`
- `04 - Post Data Date Range.bru`

### CatalogItem (ejemplo segunda entidad)
- `01 - Get Configuration.bru`
- `02 - Post Data All Types.bru`

## 3) Variables de entorno

Archivo:
- `bruno/TablaGenericaPrimeEsc/environments/local.bru`

Variables:
- `baseUrl`: URL base de tu API (ejemplo `https://localhost:5001`).
- `employeeTableBasePath`: ruta base de Employee (`/api/employee-table`).
- `catalogTableBasePath`: ruta base de CatalogItem (`/api/catalog-item-table`).

## 4) Orden recomendado de ejecucion

1. Employee - Get Configuration.
2. Employee - Post Data Initial.
3. Employee - Post Data Filters And Sort.
4. Employee - Post Data Date Range.
5. CatalogItem - Get Configuration.
6. CatalogItem - Post Data All Types.

## 5) Criterios de validacion rapida

- Configuracion responde 200 y trae `columnsInfo` y `allowedItemsPerPage`.
- Data responde 200 y trae `totalRecords`, `totalRecordsNotFiltered` y `data`.
- Cambian resultados cuando cambian filtros/sort.
- No falla timezone en filtros de fecha.

## 6) Errores frecuentes

- 404 en endpoints:
  - Revisar `employeeTableBasePath` o `catalogTableBasePath`.
- CORS:
  - Asegurar que backend permita origen de Bruno.
- 400 por payload:
  - Revisar nombres exactos de columnas en `columns` y `filter`.

## 7) Nota importante

La parte Employee deberia funcionar con los ejemplos actuales del repo.
La parte CatalogItem requiere que implementes la entidad/endpoint sugeridos en la guia de segunda entidad.
