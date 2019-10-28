import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ICliente } from '../_interfaces/cliente.interface';
import { ClienteService } from './cliente.service';
import { ToastService } from '../toast-component/toast.service';
import { Status } from '../_enums/status.enum';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.index.component.html',
  styleUrls: ['./cliente.index.component.scss']
})
export class ClienteComponent implements OnInit {
    constructor(private route: Router, private clienteService: ClienteService, private toastService: ToastService) { }

    entity: ICliente[] = [];
    cols: any;

    ngOnInit() {
        this.get();
    }

    add() {
        this.route.navigate(["cliente/create"]);
    }

    editarEntity(e: number) {
        this.route.navigate(["cliente/edit/" + e]);
    }

    excluirEntity(e: number) {
        this.toastService.showConfirm("Deseja realmente deletar este registro?", "Remoção de registro",
            "Sim", "Não", Status.Warning, this.onConfirmDelete.bind(this, e));
    }

    onConfirmDelete(entityId: number) {
        this.clienteService.delete(entityId)
            .then(response => {
                this.toastService.show(response);
                this.get();
            })
    }

    get() {
        this.clienteService.getAll()
            .then(response => {
                if (response.status === Status.Success) {
                    this.entity = response.data;
                }
            });
    }
}
