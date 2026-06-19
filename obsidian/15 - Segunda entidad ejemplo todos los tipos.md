# Segunda entidad ejemplo todos los tipos

> Objetivo: tener un segundo caso completo ademas de Employee, cubriendo todos los tipos de datos habituales de la tabla.

## 1) Escenario

Entidad propuesta: `CatalogItem`.

Campos:
- `RowID` (int o Guid): identificador unico y estable.
- `Code` (texto corto).
- `Name` (texto).
- `UnitPrice` (numerico decimal).
- `Stock` (numerico entero).
- `CreatedAt` (fecha).
- `IsEnabled` (booleano).
- `Tags` (lista separada por `;`).
- `Status` (texto con predefined).
- `StatusIconKey` (texto para icono).
- `StatusImageKey` (texto para imagen).

## 2) DTO backend

```csharp
using ECS.PrimengTable.Attributes;
using ECS.PrimengTable.Enums;

namespace Demo.Tables.CatalogItems;

public sealed class CatalogItemTableDto
{
    [ColumnAttributes(sendColumnAttributes: false)]
    public int RowID { get; set; }

    [ColumnAttributes("Codigo", canBeGlobalFiltered: true)]
    public string Code { get; set; } = string.Empty;

    [ColumnAttributes("Nombre", canBeGlobalFiltered: true)]
    public string Name { get; set; } = string.Empty;

    [ColumnAttributes("Precio", dataType: DataType.Numeric, dataAlignHorizontal: DataAlignHorizontal.Right)]
    public decimal UnitPrice { get; set; }

    [ColumnAttributes("Stock", dataType: DataType.Numeric, dataAlignHorizontal: DataAlignHorizontal.Right)]
    public int Stock { get; set; }

    [ColumnAttributes("Creado", dataType: DataType.Date)]
    public DateTime CreatedAt { get; set; }

    [ColumnAttributes("Habilitado", dataType: DataType.Boolean)]
    public bool IsEnabled { get; set; }

    [ColumnAttributes("Tags", dataType: DataType.List)]
    public string Tags { get; set; } = string.Empty; // ej: "eco;nuevo;promocion"

    [ColumnAttributes("Estado", filterPredefinedValuesName: "catalogStatus")]
    public string Status { get; set; } = string.Empty;

    [ColumnAttributes("Estado icono", filterPredefinedValuesName: "catalogStatusIcon")]
    public string StatusIconKey { get; set; } = string.Empty;

    [ColumnAttributes("Estado imagen", filterPredefinedValuesName: "catalogStatusImage")]
    public string StatusImageKey { get; set; } = string.Empty;
}
```

## 3) Predefined filters frontend (tags, iconos e imagenes)

```ts
readonly predefined: { [key: string]: IPredefinedFilter[] } = {
  catalogStatus: [
    { value: 'ACTIVE', name: 'Activo', displayTag: true, tagStyle: { background: '#DCFCE7', color: '#166534' } },
    { value: 'DRAFT', name: 'Borrador', displayTag: true, tagStyle: { background: '#FEF3C7', color: '#92400E' } },
    { value: 'ARCHIVED', name: 'Archivado', displayTag: true, tagStyle: { background: '#E5E7EB', color: '#374151' } }
  ],
  catalogStatusIcon: [
    { value: 'OK_ICON', name: 'Correcto', displayName: true, icon: 'pi pi-check-circle', iconColor: '#16a34a' },
    { value: 'WARN_ICON', name: 'Atencion', displayName: true, icon: 'pi pi-exclamation-triangle', iconColor: '#d97706' },
    { value: 'ERR_ICON', name: 'Error', displayName: true, icon: 'pi pi-times-circle', iconColor: '#dc2626' }
  ],
  catalogStatusImage: [
    { value: 'OK_IMG', imageURL: 'https://cdn.example.com/catalog-ok.png', imageWidth: 18, imageHeight: 18 },
    { value: 'WARN_IMG', imageURL: 'https://cdn.example.com/catalog-warn.png', imageWidth: 18, imageHeight: 18 },
    { value: 'ERR_IMG', imageURL: 'https://cdn.example.com/catalog-err.png', imageWidth: 18, imageHeight: 18 }
  ]
};
```

## 4) Cobertura de tipos en este ejemplo

- Texto: `Code`, `Name`, `Status`.
- Numerico: `UnitPrice`, `Stock`.
- Fecha: `CreatedAt`.
- Booleano: `IsEnabled`.
- Lista: `Tags`.
- Formato visual enriquecido: `StatusIconKey`, `StatusImageKey`.

## 5) Recomendaciones para no romper filtros

- `filterPredefinedValuesName` debe coincidir exactamente con la key en `predefinedFilters`.
- `value` de cada `IPredefinedFilter` debe coincidir con el dato real del backend.
- Si usas lista (`DataType.List`), define separador unico y estable (`;`).

## 6) Cuándo hace falta este segundo ejemplo

Recomendado cuando:
- Tu equipo implementa muchas tablas distintas.
- Necesitas estandar para todos los tipos de datos.
- Quieres prevenir errores de mapeo en iconos/imagenes/listas.

Si solo tienes tablas simples, Employee suele ser suficiente.
