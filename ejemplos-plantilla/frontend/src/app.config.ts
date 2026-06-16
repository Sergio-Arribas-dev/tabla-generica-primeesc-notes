import { ApplicationConfig } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { ECSPrimengTableHttpService, ECSPrimengTableNotificationService } from '@eternalcodestudio/primeng-table';
import { TableHttpAdapterService } from './table-http-adapter.service';
import { TableNotificationAdapterService } from './table-notification-adapter.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(),
    MessageService,
    { provide: ECSPrimengTableHttpService, useClass: TableHttpAdapterService },
    { provide: ECSPrimengTableNotificationService, useClass: TableNotificationAdapterService }
  ]
};
