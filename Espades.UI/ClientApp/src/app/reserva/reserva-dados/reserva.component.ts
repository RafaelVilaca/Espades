import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Status } from '../../_enums/status.enum';
import { ToastService } from '../../toast-component/toast.service';
import { RequestResult } from '../../_interfaces/request-result.interface';
import { SelectItem } from 'primeng/api';
import { TEXT_CONSTANT } from '../../_constants/text.constant';
import { ReservaService } from '../reserva.service';
import { ClienteService } from '../../cliente/cliente.service';
import { ProdutoService } from '../../produto/produto.service';
import { IReserva } from '../../_interfaces/reserva.interface';
import { IProduto } from '../../_interfaces/produto.interface';
import { ICliente } from '../../_interfaces/cliente.interface';

@Component({
    selector: 'app-reserva-dados',
    templateUrl: './reserva.component.html',
    styleUrls: ['./reserva.component.scss']
})
export class ReservaDadosComponent implements OnInit {
    constructor(private router: Router,
        private route: ActivatedRoute,
        private reservaService: ReservaService,
        private produtoService: ProdutoService,
        private clienteService: ClienteService,
        private toastService: ToastService) { }

    entity: IReserva = {};
    clientes: SelectItem[] = [];
    produtos: SelectItem[] = [];
    allClientes: ICliente[] = [];
    allProdutos: IProduto[] = [];
    cols: any;
    textConstant = TEXT_CONSTANT;
    pt: any;

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

        this.getClientes();
        this.getProdutos();
    }

    get(id: number) {
        this.reservaService.get(id)
            .then(response => {
                this.toastService.show(response)
                if (response.status === Status.Success) {
                    this.entity = response.data
                }
            });
    }

    getClientes() {
        this.clienteService.getAll()
            .then(response => {
                this.toastService.show(response)
                if (response.status === Status.Success) {
                    this.allClientes = [];
                    this.clientes = [];
                    this.allClientes = response.data;
                    this.allClientes.forEach(x => {
                        this.clientes.push({ label: x.id + " - " + x.nome, value: x.id });
                    });
                }
            });
    }

    getProdutos() {
        this.produtoService.getAll()
            .then(response => {
                this.toastService.show(response)
                if (response.status === Status.Success) {
                    this.allProdutos = [];
                    this.produtos = [];
                    this.allProdutos = response.data;
                    this.allProdutos.forEach(x => {
                        this.produtos.push({ label: x.id + " - " + x.descricao, value: x.id });
                    });
                }
            });
    }

    save() {
        if (!this.entity) {
            let request: RequestResult = { status: Status.Info, messages: ["Informe todos os dados"] };
            this.toastService.show(request);
        } else {
            this.reservaService.save(this.entity)
                .then(response => {
                    if (response.status === Status.Success) {
                        this.router.navigate(['reserva/index']);
                    }
                    this.toastService.show(response);
                });
        }
    }

    indexPage() {
        this.router.navigate(['reserva/index']);
    }
}
