import { Component, Input, Output, EventEmitter } from "@angular/core";
import { MessageService } from "primeng/api";

@Component({
  selector: "toast-confirmation",
  template: `
    <button *ngIf="label" type="button" (click)="showConfirm()">{{label}}</button>

    <p-toast position="center" key="{{key}}" (onClose)="onReject()" [modal]="true" [baseZIndex]="12000">
        <ng-template let-message pTemplate="message">
            <div style="text-align: center">
                <i class="pi {{icon}}" style="font-size: 3em"></i>
                <h3>{{message.summary}}</h3>
                <p>{{message.detail}}</p>
            </div>
            <div class="ui-g ui-fluid">
                <div class="ui-g-6">
                    <button type="button" pButton (click)="onConfirm()" label="{{btnTextYes}}" class="btn btn-success"></button>
                </div>
                <div class="ui-g-6">
                    <button type="button" pButton (click)="onReject()" label="{{btnTextNo}}" class="btn btn-danger"></button>
                </div>
            </div>
        </ng-template>
    </p-toast>
  `
})
export class ToastConfirmationComponent {
  public key: string;
  private isCreated: boolean = false;

  constructor(private messageService: MessageService) {
    this.key = new Date().getUTCMilliseconds().toString();
  }

  @Input() summary = "";
  @Input() detail = "";
  @Input() label = "";
  @Input() btnTextYes = "Sim";
  @Input() btnTextNo = "Não";
  @Input() icon: string = "pi-exclamation-triangle";
  @Input() status: string = "warn";
  @Output() onConfirmCallback = new EventEmitter();
  @Output() onRejectCallback = new EventEmitter();

  showConfirm() {
    if (!this.isCreated) {
      this.isCreated = true;
      this.messageService.add({ severity: this.status, key: this.key, summary: this.summary, detail: this.detail, sticky: true });
    }
  }

  onConfirm() {
    if (!this.label) {
      (<any>this.onConfirmCallback)();
    } else {
      this.onConfirmCallback.emit();
    }

    this.messageService.clear(this.key);
    this.isCreated = false;
  }

  onReject() {
    if (this.onRejectCallback !== undefined) {
      if (!this.label) {
        (<any>this.onRejectCallback)();
      } else {
        this.onRejectCallback.emit();
      }
    }

    this.messageService.clear(this.key);
    this.isCreated = false;
  }
}

