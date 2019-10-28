import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Status } from '../../_enums/status.enum';
import { ToastService } from '../../toast-component/toast.service';
import { RequestResult } from '../../_interfaces/request-result.interface';
import { TEXT_CONSTANT } from '../../_constants/text.constant';
import { PatrimonioService } from '../patrimonio.service';
import { IPatrimonio } from '../../_interfaces/patrimonio.interface';
import { SelectItem } from 'primeng/api';

@Component({
    selector: 'app-patrimonio-dados',
    templateUrl: './patrimonio.component.html',
    styleUrls: ['./patrimonio.component.scss']
})
export class PatrimonioDadosComponent implements OnInit {
    constructor(private router: Router,
        private route: ActivatedRoute,
        private patrimonioService: PatrimonioService,
        private toastService: ToastService) { }

    entity: IPatrimonio = {};
    situacoes: SelectItem[] = [];
    pt: any = {};
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

        this.pt = {
            firstDayOfWeek: 0,
            dayNames: ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"],
            dayNamesShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb"],
            dayNamesMin: ["Do", "Se", "Te", "Qa", "Qi", "Se", "Sa"],
            monthNames: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
            monthNamesShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
            today: 'Hoje',
            clear: 'Limpar',
            dateFormat: 'mm/dd/yy',
            weekHeader: 'Sem'
        };

        this.situacoes = [{ label: "Ok", value: "Ok" },
            { label: "Atenção", value: "Atenção" },
            { label: "Crítico", value: "Crítico" }]
    }

    get(id: number) {
        this.patrimonioService.get(id)
            .then(response => {
                this.toastService.show(response)
                if (response.status === Status.Success) {
                    this.entity = response.data
                }
            });
    }

    save() {
        this.entity.valor = Number(this.entity.valor.toString().replace(new RegExp(`^(.{${this.entity.valor.toString().length - 2}})(.)`), `$1${'.'}$2`));
        if (!this.entity) {
            let request: RequestResult = { status: Status.Info, messages: ["Informe todos os dados"] };
            this.toastService.show(request);
        } else {
            this.patrimonioService.save(this.entity)
                .then(response => {
                    if (response.status === Status.Success) {
                        this.router.navigate(['patrimonio/index']);
                    }
                    this.toastService.show(response);
                });
        }
    }

    indexPage() {
        this.router.navigate(['patrimonio/index']);
    }
}
