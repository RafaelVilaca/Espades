import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EstoqueService } from './estoque.service';
import { ToastService } from '../toast-component/toast.service';
import { IEstoque } from '../_interfaces/estoque.interface';
import { Status } from '../_enums/status.enum';

@Component({
  selector: 'app-estoque',
  templateUrl: './estoque.index.component.html',
  styleUrls: ['./estoque.index.component.scss']
})
export class EstoqueComponent implements OnInit {
    constructor(private route: Router, private estoqueService: EstoqueService, private toastService: ToastService) { }

    entity: IEstoque[] = [];
    cols: any;

    ngOnInit() {
        this.get();
    }

    add() {
        this.route.navigate(["estoque/create"]);
    }

    editarEntity(e: number) {
        this.route.navigate(["estoque/edit/" + e]);
    }

    excluirEntity(e: number) {
        this.toastService.showConfirm("Deseja realmente deletar este registro?", "Remoção de registro",
            "Sim", "Não", Status.Warning, this.onConfirmDelete.bind(this, e));
    }

    onConfirmDelete(entityId: number) {
        this.estoqueService.delete(entityId)
            .then(response => {
                this.toastService.show(response);
                this.get();
            })
    }

    get() {
        this.estoqueService.getAll()
            .then(response => {
                if (response.status === Status.Success) {
                    this.entity = response.data;
                }
            });
    }
}
