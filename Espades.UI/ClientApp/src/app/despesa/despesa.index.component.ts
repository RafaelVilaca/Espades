import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DespesaService } from './despesa.service';
import { ToastService } from '../toast-component/toast.service';
import { IDespesa } from '../_interfaces/despesa.interface';
import { Status } from '../_enums/status.enum';

@Component({
  selector: 'app-despesa',
  templateUrl: './despesa.index.component.html',
  styleUrls: ['./despesa.index.component.scss']
})
export class DespesaComponent implements OnInit {
    constructor(private route: Router, private despesaService: DespesaService, private toastService: ToastService) { }

    entity: IDespesa[] = [];
    cols: any;

    ngOnInit() {
        this.get();
    }

    add() {
        this.route.navigate(["despesa/create"]);
    }

    editarEntity(e: number) {
        this.route.navigate(["despesa/edit/" + e]);
    }

    excluirEntity(e: number) {
        this.toastService.showConfirm("Deseja realmente deletar este registro?", "Remoção de registro",
            "Sim", "Não", Status.Warning, this.onConfirmDelete.bind(this, e));
    }

    onConfirmDelete(entityId: number) {
        this.despesaService.delete(entityId)
            .then(response => {
                this.toastService.show(response);
                this.get();
            })
    }

    get() {
        this.despesaService.getAll()
            .then(response => {
                if (response.status === Status.Success) {
                    this.entity = response.data;
                }
            });
    }
}
