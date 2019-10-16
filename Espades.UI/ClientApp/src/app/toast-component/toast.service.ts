import { ApplicationRef, ComponentFactoryResolver, Inject, Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AppComponent } from '../app.component';
import { Status } from '../_enums/status.enum';
import { RequestResult } from '../_interfaces/request-result.interface';
import { ToastConfirmationComponent } from './toast.confirmation.component';

@Injectable({
  providedIn: 'root'
})

export class ToastService {
  private factoryResolver: ComponentFactoryResolver;
  private rootViewContainer: any;
  public toastConfirmation: ToastConfirmationComponent;

  constructor(private messageService: MessageService,
    @Inject(ComponentFactoryResolver) factoryResolver,
    private appRef: ApplicationRef) {
    this.factoryResolver = factoryResolver;
    if (appRef.components[0]) {
      this.rootViewContainer = (appRef.components[0].instance as AppComponent).viewRef;
      this.addToastConfirmationComponent();
    }
  }

  public initialize(viewRef: any): void {
    this.rootViewContainer = viewRef;
    this.addToastConfirmationComponent();
  }

  show(response: RequestResult) {
    if (response.messages && response.messages.length > 0) {
      for (let i = 0; i < response.messages.length; i++) {
        let message = response.messages[i];

        let config = this.getSeverity(response.status);

        this.messageService.add({ severity: config.type, summary: config.summary, detail: message });
      }
    }
  }

  private getSeverity(status: Status): any {
    let result = {
      type: '',
      summary: '',
      icon: ''
    };
    switch (status) {
      case Status.Success: {
        result.type = "success";
        result.summary = "Sucesso";
        result.icon = "pi-check";
        break;
      }
      case Status.Info: {
        result.type = "info";
        result.summary = "Informação";
        result.icon = "pi-info-circle";
        break;
      }
      case Status.Warning: {
        result.type = "warn";
        result.summary = "Atenção";
        result.icon = "pi-exclamation-triangle";
        break;
      }
      case Status.Error: {
        result.type = "error";
        result.summary = "Erro";
        result.icon = "pi-times";
        break;
      }
    }
    return result;
  }

  private addToastConfirmationComponent() {
    const factory = this.factoryResolver
      .resolveComponentFactory(ToastConfirmationComponent);
    const component = factory
      .create(this.rootViewContainer.parentInjector);
    this.rootViewContainer.insert(component.hostView);
    this.toastConfirmation = component.instance;
  }

  showConfirm(title: string, detail: string,
    btnTextYes: string, btnTextNo: string,
    status: Status, onConfirmCallback: any,
    onRejectCallback: any = undefined) {

    this.toastConfirmation.summary = title;
    this.toastConfirmation.detail = detail;
    this.toastConfirmation.btnTextYes = btnTextYes;
    this.toastConfirmation.btnTextNo = btnTextNo;
    this.toastConfirmation.onConfirmCallback = onConfirmCallback;
    this.toastConfirmation.onRejectCallback = onRejectCallback;
    let config = this.getSeverity(status);
    this.toastConfirmation.status = config.type;
    this.toastConfirmation.icon = config.icon;
    this.toastConfirmation.showConfirm();
  }

  clearAll() {
    this.messageService.clear();
  }
}
