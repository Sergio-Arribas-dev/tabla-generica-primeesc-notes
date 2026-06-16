import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { ECSPrimengTableNotificationService } from '@eternalcodestudio/primeng-table';

@Injectable({ providedIn: 'root' })
export class TableNotificationAdapterService extends ECSPrimengTableNotificationService {
  constructor(private messageService: MessageService) {
    super();
  }

  showToast(severity: string, title: string, message: string): void {
    this.messageService.add({ severity, summary: title, detail: message, life: 4500 });
  }

  clearToasts(): void {
    this.messageService.clear();
  }
}
