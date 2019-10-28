import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastService } from '../toast-component/toast.service';
import { SetorService } from './setor.service';
import { Status } from '../_enums/status.enum';
import { ISetor } from '../_interfaces/setor.interface';

@Component({
  selector: 'app-setor',
  templateUrl: './setor.index.component.html',
  styleUrls: ['./setor.index.component.scss']
})
export class SetorComponent implements OnInit {
    constructor(private route: Router, private setorService: SetorService, private toastService: ToastService) { }

    entity: ISetor = null;

    ngOnInit() {
        this.get();
    }

    add() {
        this.route.navigate(["setor/create"]);
    }

    editarEntity(e: number) {
        this.route.navigate(["setor/edit/" + e]);
    }

    excluirEntity(e: number) {
        this.toastService.showConfirm("Deseja realmente deletar este registro?", "Remoção de registro",
            "Sim", "Não", Status.Warning, this.onConfirmDelete.bind(this, e));
    }

    onConfirmDelete(entityId: number) {
        this.setorService.delete(entityId)
            .then(response => {
                this.toastService.show(response);
                this.get();
            })
    }

    get() {
        this.setorService.getAll()
            .then(response => {
                if (response.status === Status.Success) {
                    this.entity = response.data;
                }
            });
    }
}
