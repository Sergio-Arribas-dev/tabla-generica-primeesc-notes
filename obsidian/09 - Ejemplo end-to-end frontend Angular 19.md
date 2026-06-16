# Ejemplo end-to-end frontend (Angular 19)

> Continuación del mismo caso de backend: **Employee Table**.

## 1) Tipos mínimos de la pantalla
```ts
export interface EmployeeRow {
  rowID: string;
  username: string;
  salary: number;
  birthDate: string | null;
  employmentStatus: string;
  hasHouse: boolean;
  statusColorHex: string;
}
```

## 2) Adapter HTTP (obligatorio)
```ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ECSPrimengTableHttpService } from '@eternalcodestudio/primeng-table';

@Injectable({ providedIn: 'root' })
export class TableHttpAdapterService extends ECSPrimengTableHttpService {
  private readonly apiBaseUrl = 'https://localhost:5001/api/';

  constructor(private readonly http: HttpClient) {
    super();
  }

  handleHttpGetRequest<T>(servicePoint: string, responseType: 'json' | 'blob' = 'json'): Observable<HttpResponse<T>> {
    return this.http.get<T>(`${this.apiBaseUrl}${servicePoint}`, {
      observe: 'response',
      responseType: responseType as 'json'
    });
  }

  handleHttpPostRequest<T>(
    servicePoint: string,
    data: any,
    httpOptions: HttpHeaders | null = null,
    responseType: 'json' | 'blob' = 'json'
  ): Observable<HttpResponse<T>> {
    return this.http.post<T>(`${this.apiBaseUrl}${servicePoint}`, data, {
      headers: httpOptions ?? undefined,
      observe: 'response',
      responseType: responseType as 'json'
    });
  }
}
```

## 3) Adapter notificaciones (obligatorio)
```ts
import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { ECSPrimengTableNotificationService } from '@eternalcodestudio/primeng-table';

@Injectable({ providedIn: 'root' })
export class TableNotificationAdapterService extends ECSPrimengTableNotificationService {
  constructor(private readonly messageService: MessageService) {
    super();
  }

  showToast(severity: string, title: string, message: string): void {
    this.messageService.add({ severity, summary: title, detail: message, life: 4500 });
  }

  clearToasts(): void {
    this.messageService.clear();
  }
}
```

## 4) app.config.ts
```ts
import { ApplicationConfig } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { ECSPrimengTableHttpService, ECSPrimengTableNotificationService } from '@eternalcodestudio/primeng-table';
import { TableHttpAdapterService } from './shared/table-http-adapter.service';
import { TableNotificationAdapterService } from './shared/table-notification-adapter.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(),
    MessageService,
    { provide: ECSPrimengTableHttpService, useClass: TableHttpAdapterService },
    { provide: ECSPrimengTableNotificationService, useClass: TableNotificationAdapterService }
  ]
};
```

## 5) Componente de la pantalla
```ts
import { Component } from '@angular/core';
import {
  ECSPrimengTable,
  IPredefinedFilter,
  ITableOptions,
  createTableOptions,
  DataAlignHorizontal,
  DataAlignVertical
} from '@eternalcodestudio/primeng-table';

@Component({
  selector: 'app-employee-table-page',
  standalone: true,
  imports: [ECSPrimengTable],
  templateUrl: './employee-table-page.component.html'
})
export class EmployeeTablePageComponent {
  readonly predefined: { [key: string]: IPredefinedFilter[] } = {
    employmentStatus: [
      { value: 'Full-time', name: 'Full-time', displayTag: true, tagStyle: { background: '#DCFCE7', color: '#166534' } },
      { value: 'Part-time', name: 'Part-time', displayTag: true, tagStyle: { background: '#FEF3C7', color: '#92400E' } },
      { value: 'Contract', name: 'Contract', displayTag: true, tagStyle: { background: '#DBEAFE', color: '#1E40AF' } }
    ]
  };

  tableOptions: ITableOptions = createTableOptions({
    urlTableConfiguration: 'employee-table/configuration',
    urlTableData: 'employee-table/data',
    predefinedFilters: this.predefined,
    header: {
      clearFiltersEnabled: true,
      clearSortsEnabled: true
    },
    rows: {
      singleSelector: {
        enabled: true,
        metakey: false
      },
      action: {
        header: 'Acciones',
        frozen: true,
        positionRight: true,
        width: 140,
        horizontalAlignment: DataAlignHorizontal.Center,
        verticalAlignment: DataAlignVertical.Middle,
        buttons: [
          {
            icon: 'pi pi-search',
            tooltip: 'Ver detalle',
            action: (rowData: any) => this.openDetail(rowData)
          }
        ]
      }
    },
    globalFilter: {
      enabled: true,
      maxLength: 30
    }
  });

  onDataUpdated(): void {
    console.log('Tabla actualizada');
  }

  private openDetail(rowData: any): void {
    console.log('Detalle empleado', rowData.rowID);
  }
}
```

## 6) Template HTML
```html
<ecs-primeng-table
  [tableOptions]="tableOptions"
  (onDataEndUpdate)="onDataUpdated()">
</ecs-primeng-table>
```

## 7) CSS opcional para filas dinámicas
```ts
rows: {
  class: (rowData: any) => ({
    'row-expensive': rowData.salary > 70000,
    'row-warning': !rowData.hasHouse
  })
}
```

```css
.row-expensive td {
  border-left: 3px solid #16a34a;
}

.row-warning td {
  background: #fff7ed;
}
```

## 8) Checklist frontend rápido
- ✓ Usar `createTableOptions()` (nunca `{}` directo)
- ✓ Registrar adapters HTTP y Notification en `app.config.ts`
- ✓ Implementar `ECSPrimengTableHttpService` (GET + POST)
- ✓ Implementar `ECSPrimengTableNotificationService` (showToast + clearToasts)
- ✓ URLs endpoints sin `/api/` base (adapter lo añade)
- ✓ No duplicar lógica de filtro en cliente
- ✓ Usar `onDataEndUpdate` para sincronizaciones post-fetch
- ✓ Mantener `tableOptions` inmutable (no hacer `tableOptions.header = ...`)
- ✓ Validar CORS en backend si front ≠ backend port
- ✓ Tests: mock adapters, verificar `createTableOptions` construcción
