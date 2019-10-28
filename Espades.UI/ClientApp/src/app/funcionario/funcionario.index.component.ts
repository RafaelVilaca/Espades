import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastService } from '../toast-component/toast.service';
import { FuncionarioService } from './funcionario.service';
import { Status } from '../_enums/status.enum';
import { IFuncionario } from '../_interfaces/funcionario.interface';

@Component({
    selector: 'app-funcionario',
    templateUrl: './funcionario.index.component.html',
    styleUrls: ['./funcionario.index.component.scss']
})
export class FuncionarioComponent implements OnInit {
    constructor(private route: Router, private funcionarioService: FuncionarioService, private toastService: ToastService) { }

    entity: IFuncionario[] = [];
    cols: any;

    ngOnInit() {
        this.get();
    }

    add() {
        this.route.navigate(["funcionario/create"]);
    }

    editarEntity(e: number) {
        this.route.navigate(["funcionario/edit/" + e]);
    }

    excluirEntity(e: number) {
        this.toastService.showConfirm("Deseja realmente deletar este registro?", "Remoção de registro",
            "Sim", "Não", Status.Warning, this.onConfirmDelete.bind(this, e));
    }

    onConfirmDelete(entityId: number) {
        this.funcionarioService.delete(entityId)
            .then(response => {
                this.toastService.show(response);
                this.get();
            })
    }

    get() {
        this.funcionarioService.getAll()
            .then(response => {
                if (response.status === Status.Success) {
                    this.entity = response.data;
                }
            });
    }
}
