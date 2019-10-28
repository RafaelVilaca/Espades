import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Status } from '../../_enums/status.enum';
import { ToastService } from '../../toast-component/toast.service';
import { RequestResult } from '../../_interfaces/request-result.interface';
import { SelectItem } from 'primeng/api';
import { TEXT_CONSTANT } from '../../_constants/text.constant';
import { EstoqueService } from '../estoque.service';
import { ProdutoService } from '../../produto/produto.service';
import { IEstoque } from '../../_interfaces/estoque.interface';
import { IProduto } from '../../_interfaces/produto.interface';

@Component({
    selector: 'app-estoque-dados',
    templateUrl: './estoque.component.html',
    styleUrls: ['./estoque.component.scss']
})
export class EstoqueDadosComponent implements OnInit {
    constructor(private router: Router,
        private route: ActivatedRoute,
        private estoqueService: EstoqueService,
        private produtoService: ProdutoService,
        private toastService: ToastService) { }

    entity: IEstoque = {};
    produtos: SelectItem[] = [];
    allProdutos: IProduto[] = [];
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

        this.getProdutos();
    }

    get(id: number) {
        this.estoqueService.get(id)
            .then(response => {
                this.toastService.show(response)
                if (response.status === Status.Success) {
                    this.entity = response.data
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
        this.entity.valor = Number(this.entity.valor.toString().replace(new RegExp(`^(.{${this.entity.valor.toString().length - 2}})(.)`), `$1${'.'}$2`));
        if (!this.entity) {
            let request: RequestResult = { status: Status.Info, messages: ["Informe todos os dados"] };
            this.toastService.show(request);
        } else {
            this.estoqueService.save(this.entity)
                .then(response => {
                    if (response.status === Status.Success) {
                        this.router.navigate(['estoque/index']);
                    }
                    this.toastService.show(response);
                });
        }
    }

    indexPage() {
        this.router.navigate(['estoque/index']);
    }
}
