import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ISetor } from '../../_interfaces/setor.interface';
import { SetorService } from '../setor.service';
import { Status } from '../../_enums/status.enum';
import { ToastService } from '../../toast-component/toast.service';
import { RequestResult } from '../../_interfaces/request-result.interface';
import { TEXT_CONSTANT } from '../../_constants/text.constant';
import { SelectItem } from 'primeng/api';
import { TabOptions } from '../../_enums/tab-options.enum';
import { CargoService } from '../../cargo/cargo.service';
import { ICargo } from '../../_interfaces/cargo.interface';

@Component({
    selector: 'app-setor-dados',
    templateUrl: './setor.component.html',
    styleUrls: ['./setor.component.scss']
})
export class SetorDadosComponent implements OnInit {
    constructor(private router: Router,
        private route: ActivatedRoute,
        private cargoService: CargoService,
        private toastService: ToastService,
        private setorService: SetorService) { }

    entity: ISetor = {};
    cargo: ICargo = {};
    optionButton: any;
    oldOptionButton: any;
    types: SelectItem[] = [];
    textConstant = TEXT_CONSTANT;

    ngOnInit() {
        this.route.paramMap.subscribe((data: any) => {
            if (data.params !== undefined && data.params.id !== undefined) {
                let uId = data.params.id;
                if (uId != null && uId > 0) {
                    this.get(uId);
                }
            }
        });

        this.types = [
            { label: 'Dados', value: TabOptions.Data, icon: 'pi pi-users' },
            { label: 'Cargos', value: TabOptions.Cargo, icon: 'pi pi-briefcase' }
        ];

        this.optionButton = TabOptions.Data;
        this.oldOptionButton = TabOptions.Data;
    }

    get(id: number) {
        this.setorService.get(id)
            .then(response => {
                this.toastService.show(response)
                if (response.status === Status.Success) {
                    this.entity = response.data
                }
            });
    }

    save(redirect: boolean) {
        if (!this.entity) {
            let request: RequestResult = { status: Status.Info, messages: ["Informe todos os dados"] };
            this.toastService.show(request);
        } else {
            this.setorService.save(this.entity)
                .then(response => {
                    if (response.status === Status.Success) {
                        if (redirect) {
                            this.router.navigate(['setor/index']);
                        }
                    }
                    this.toastService.show(response);
                });
        }
    }

    onChangeOption() {
        if (this.oldOptionButton === TabOptions.Data && this.optionButton !== TabOptions.Data) {
            this.save(false);
        } else {
            this.allowTabChange();
        }

        if (this.entity.id && this.entity.id > 0) {
            if (this.optionButton === TabOptions.Cargo) {
            }
        }
    }

    allowTabChange() {
        this.oldOptionButton = this.optionButton;
    }

    saveCargo() {
        this.cargo.id_Setor = this.entity.id;
        if (!this.cargo) {
            let request: RequestResult = { status: Status.Info, messages: ["Informe todos os dados do endereço"] };
            this.toastService.show(request);
        } else {
            this.cargoService.save(this.cargo)
                .then(response => {
                    if (response.status === Status.Success) {
                        this.get(this.entity.id);
                        this.cargo = {};
                    }
                    this.toastService.show(response);
                });
        }
    }

    editarCargo(e: number) {
        this.cargoService.get(e)
            .then(response => {
                if (response.status === Status.Success) {
                    this.cargo = response.data;
                }
                this.toastService.show(response);
            });
    }

    excluirEndereco(e: number) {
        this.toastService.showConfirm("Deseja realmente deletar este registro?", "Remoção de registro",
            "Sim", "Não", Status.Warning, this.onConfirmDelete.bind(this, e));
    }

    onConfirmDelete(entityId: number) {
        this.cargoService.delete(entityId)
            .then(response => {
                this.toastService.show(response);
                this.get(this.entity.id)
            });
    }

    indexPage() {
        this.router.navigate(['setor/index']);
    }
}
