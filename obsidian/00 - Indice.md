# Índice — Tabla genérica basada en PrimeNG (Angular 14/19 + .NET 8)

> Objetivo: tener una guía de implementación y mantenimiento **reutilizable**, con enfoque en **SOLID**, **KISS** y rendimiento para tablas con **lazy loading** y lógica de consulta en base de datos.

## Cómo usar estos apuntes
1. Leer primero [[11 - Guia maestra implementacion repetible]] (ruta recomendada).
2. Leer [[01 - Arquitectura y flujo]] para entender la visión global.
3. Implementar backend con [[02 - Backend .NET 8]].
4. Implementar frontend con [[03 - Frontend Angular 19]].
5. Alinear contratos en [[04 - Contratos API y modelos]].
6. Ejecutar checklist de [[05 - Plan de implementación y checklist]].
7. Revisar [[06 - Operación, rendimiento y troubleshooting]] cuando algo falle.
8. Copiar la implementación de referencia en [[08 - Ejemplo end-to-end backend .NET 8]].
9. Copiar la implementación de referencia en [[09 - Ejemplo end-to-end frontend Angular 19]].
10. Probar payloads reales en [[10 - Payloads reales y casos de referencia]].
11. Aterrizar filtros complejos con [[13 - Guia avanzada de filtros]].
12. Repetir implementaciones rapido con [[12 - Plantilla nueva tabla en 20 minutos]].
13. Probar API con [[14 - Coleccion Bruno y pruebas API]].
14. Revisar ejemplo completo de segunda entidad en [[15 - Segunda entidad ejemplo todos los tipos]].
15. Implementar calidad con [[16 - Guia testing automatizado]].

## Alcance
- Base tomada del documento raíz del proyecto.
- Adaptado a stack habitual: **ASP.NET Core 8 + Angular 14/19**.
- Patrón recomendado: `Controller -> Service -> Repository`.

## Principios de diseño aplicados
- **S (SRP)**: cada capa tiene una responsabilidad única.
- **O (OCP)**: columnas y filtros extensibles por metadatos.
- **L (LSP)**: servicios abstractos de HTTP/notificación reemplazables.
- **I (ISP)**: contratos pequeños para vistas, query y export.
- **D (DIP)**: frontend depende de abstracciones; backend también.
- **KISS**: un endpoint de configuración + un endpoint de datos como núcleo mínimo.

## Nota de compatibilidad
La documentaci\u00f3n original es para versiones m\u00e1s nuevas. Para **Angular 19 + .NET 8**:
- Revisar versi\u00f3n de PrimeNG compatible con Angular 19 (este repo usa \u226519.0.0).
- Validar que adapters HTTP/Notification se registren correctamente en DI.
- Evitar usar APIs internas del paquete `@eternalcodestudio/primeng-table`.
- Si tienes issues de versiones, referirse a [ejemplos-plantilla/frontend/package.json](../ejemplos-plantilla/frontend/package.json).
