import { Component } from '@angular/core';
import {
  ECSPrimengTable,
  ITableOptions,
  createTableOptions,
  DataAlignHorizontal,
  IPredefinedFilter
} from '@eternalcodestudio/primeng-table';

@Component({
  selector: 'app-employee-table-page',
  standalone: true,
  imports: [ECSPrimengTable],
  template: `<ecs-primeng-table [tableOptions]="tableOptions" (onDataEndUpdate)="onDataUpdated()"></ecs-primeng-table>`
})
export class EmployeeTablePageComponent {
  readonly predefined: { [key: string]: IPredefinedFilter[] } = {
    employmentStatus: [
      { value: 'Full-time', name: 'Full-time', displayTag: true, tagStyle: { background: '#DCFCE7', color: '#166534' } },
      { value: 'Part-time', name: 'Part-time', displayTag: true, tagStyle: { background: '#FEF3C7', color: '#92400E' } }
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
      singleSelector: { enabled: true },
      action: {
        header: 'Acciones',
        buttons: [
          {
            icon: 'pi pi-search',
            tooltip: 'Detalles',
            action: (row: any) => console.log('Detail:', row.rowID)
          }
        ]
      }
    },
    globalFilter: { enabled: true, maxLength: 30 }
  });

  onDataUpdated(): void {
    console.log('Tabla actualizada');
  }
}
