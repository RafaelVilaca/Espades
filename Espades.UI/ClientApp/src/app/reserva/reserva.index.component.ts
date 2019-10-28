import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReservaService } from './reserva.service';
import { ToastService } from '../toast-component/toast.service';
import { IReserva } from '../_interfaces/reserva.interface';
import { Status } from '../_enums/status.enum';

@Component({
    selector: 'app-reserva',
    templateUrl: './reserva.index.component.html',
    styleUrls: ['./reserva.index.component.scss']
})
export class ReservaComponent implements OnInit {
    constructor(private route: Router, private reservaService: ReservaService, private toastService: ToastService) { }

    entity: IReserva[] = [];
    cols: any;

    ngOnInit() {
        this.get();
    }

    add() {
        this.route.navigate(["reserva/create"]);
    }

    editarEntity(e: number) {
        this.route.navigate(["reserva/edit/" + e]);
    }

    excluirEntity(e: number) {
        this.toastService.showConfirm("Deseja realmente deletar este registro?", "Remoção de registro",
            "Sim", "Não", Status.Warning, this.onConfirmDelete.bind(this, e));
    }

    onConfirmDelete(entityId: number) {
        this.reservaService.delete(entityId)
            .then(response => {
                this.toastService.show(response);
                this.get();
            })
    }

    get() {
        this.reservaService.getAll()
            .then(response => {
                if (response.status === Status.Success) {
                    this.entity = response.data;
                }
            });
    }
}
