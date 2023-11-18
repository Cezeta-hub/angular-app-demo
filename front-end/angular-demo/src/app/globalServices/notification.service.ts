import { Injectable } from '@angular/core';
import {MessageService} from 'primeng/api';
@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private messageService: MessageService) { }

  public ShowSuccess(mensajePrincipal: string, mensajeSecundario?: string) {
    this.messageService.add({severity:'success', summary: mensajePrincipal, detail: mensajeSecundario});
  }
  public ShowError(mensajePrincipal: string, mensajeSecundario?: string) {
    this.messageService.add({severity:'error', summary: mensajePrincipal, detail: mensajeSecundario});
  }
  public ShowWarning(mensajePrincipal: string, mensajeSecundario?: string) {
    this.messageService.add({severity:'warn', summary: mensajePrincipal, detail: mensajeSecundario});
  }
  public ShowInfo(mensajePrincipal: string, mensajeSecundario?: string) {
    this.messageService.add({severity:'info', summary: mensajePrincipal, detail: mensajeSecundario});
  }
}
