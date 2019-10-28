import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastService } from '../toast-component/toast.service';
import { PatrimonioService } from './patrimonio.service';
import { IPatrimonio } from '../_interfaces/patrimonio.interface';
import { Status } from '../_enums/status.enum';

@Component({
  selector: 'app-patrimonio',
  templateUrl: './patrimonio.index.component.html',
  styleUrls: ['./patrimonio.index.component.scss']
})
export class PatrimonioComponent implements OnInit {
    constructor(private route: Router, private patrimonioService: PatrimonioService, private toastService: ToastService) { }

    entity: IPatrimonio[] = [];
    cols: any;

    ngOnInit() {
        this.get();
    }

    add() {
        this.route.navigate(["patrimonio/create"]);
    }

    editarEntity(e: number) {
        this.route.navigate(["patrimonio/edit/" + e]);
    }

    excluirEntity(e: number) {
        this.toastService.showConfirm("Deseja realmente deletar este registro?", "Remoção de registro",
            "Sim", "Não", Status.Warning, this.onConfirmDelete.bind(this, e));
    }

    onConfirmDelete(entityId: number) {
        this.patrimonioService.delete(entityId)
            .then(response => {
                this.toastService.show(response);
                this.get();
            })
    }

    getColor(country: string) {
        switch (country) {
            case 'Ok':
                return 'green';
            case 'Atenção':
                return 'gold';
            case 'Crítico':
                return 'red';
        }
    }

    get() {
        this.patrimonioService.getAll()
            .then(response => {
                if (response.status === Status.Success) {
                    this.entity = response.data;
                }
            });
    }
}
