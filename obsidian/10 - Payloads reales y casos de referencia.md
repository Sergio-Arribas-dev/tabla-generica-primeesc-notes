# Payloads reales y casos de referencia

## 1) Request inicial típico
```json
{
  "page": 1,
  "pageSize": 25,
  "columns": ["rowID", "username", "salary", "birthDate", "employmentStatus", "hasHouse"],
  "sort": [],
  "filter": {},
  "globalFilter": null,
  "dateFormat": "dd/MM/yyyy HH:mm",
  "dateTimezone": "+01:00",
  "dateCulture": "es-ES",
  "exportDateFormat": "dd/mm/yyyy hh:mm"
}
```

## 2) Request con filtros + sort
```json
{
  "page": 2,
  "pageSize": 25,
  "columns": ["rowID", "username", "salary", "employmentStatus", "hasHouse"],
  "sort": [
    { "field": "salary", "order": -1 },
    { "field": "username", "order": 1 }
  ],
  "filter": {
    "employmentStatus": [
      { "matchMode": "equals", "operator": "AND", "value": "Full-time" }
    ],
    "salary": [
      { "matchMode": "gte", "operator": "AND", "value": 30000 }
    ]
  },
  "globalFilter": "ser",
  "dateFormat": "dd/MM/yyyy HH:mm",
  "dateTimezone": "+01:00",
  "dateCulture": "es-ES",
  "exportDateFormat": "dd/mm/yyyy hh:mm"
}
```

## 3) Response esperado
```json
{
  "page": 2,
  "totalRecords": 133,
  "totalRecordsNotFiltered": 18457,
  "data": [
    {
      "rowID": "d3d95f75-8bcb-4db2-a0e4-779f9c5c1c6e",
      "username": "sergio.arribas",
      "salary": 56000,
      "employmentStatus": "Full-time",
      "hasHouse": true
    }
  ]
}
```

## 4) Snippet prueba rápida en integración (xUnit)
```csharp
[Fact]
public async Task GetData_Returns_Ok_And_Data()
{
    // Arrange
    var client = _factory.CreateClient();
    var payload = new
    {
        page = 1,
        pageSize = 25,
        columns = new[] { "rowID", "username", "salary" },
        sort = Array.Empty<object>(),
        filter = new { },
        globalFilter = (string?)null,
        dateFormat = "dd/MM/yyyy HH:mm",
        dateTimezone = "+01:00",
        dateCulture = "es-ES",
        exportDateFormat = "dd/mm/yyyy hh:mm"
    };

    // Act
    var response = await client.PostAsJsonAsync("/api/employee-table/data", payload);

    // Assert
    response.EnsureSuccessStatusCode();
    var json = await response.Content.ReadAsStringAsync();
    Assert.Contains("totalRecords", json);
    Assert.Contains("data", json);
}
```

## 5) Prueba manual para tus futuras implementaciones
1. Abrir Swagger.
2. Ejecutar configuración.
3. Ejecutar data con payload inicial.
4. Añadir sort/filtros y comprobar:
   - cambia `totalRecords`
   - `page` se corrige si queda fuera de rango
   - `data` solo trae columnas solicitadas

## 6) Convención recomendada de nombres
- **Backend DTO:** PascalCase (`RowID`, `Username`, `BirthDate`)
- **Frontend JSON:** camelCase (automático en Angular `HttpClient`)
- **Mapeando:** El paquete maneja conversión automáticamente
- **Regla:** Mantén nombre del property del DTO = `field` en requests
- **Ejemplo:** DTO prop `BirthDate` → request field `"birthDate"` (Angular lo convierte)
- **Evita:** Renombrar sin versionado o documentación de cambios
