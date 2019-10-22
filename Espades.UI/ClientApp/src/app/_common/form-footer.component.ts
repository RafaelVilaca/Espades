import { Component, EventEmitter, Output, Input } from "@angular/core";

@Component({
  selector: "form-footer",
  template: `
    <div class="box-footer">
      <button type="submit" class="btn btn-full-on-small btn-success btn-submit" (click)="saveClick()" *ngIf="!hideSubmit">
          <i class="pi {{saveBtnIcon}} position-icon-btn"></i>
          {{saveBtnLabel}}
        </button>

      <button type="button" class="btn btn-full-on-small btn-danger btn-cancel" (click)="backClick()">
          <i class="pi pi-arrow-circle-left position-icon-btn"></i>
          Voltar
        </button>
    </div>
  `
})
export class FormFooterComponent {
  @Input() hideSubmit: boolean = false;
  @Input() saveBtnLabel: string = "Salvar";
  @Input() saveBtnIcon: string = "pi-check";
  @Output() onBackClick: EventEmitter<any> = new EventEmitter();
  @Output() onSaveClick: EventEmitter<any> = new EventEmitter();
  constructor() { }

  public backClick(): void {
    if (this.onBackClick) {
      this.onBackClick.emit();
    }
  }

  public saveClick(): void {
    if (this.onSaveClick) {
      this.onSaveClick.emit();
    }
  }
}
