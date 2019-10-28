import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Status } from '../../_enums/status.enum';
import { ToastService } from '../../toast-component/toast.service';
import { RequestResult } from '../../_interfaces/request-result.interface';
import { SelectItem } from 'primeng/api';
import { TEXT_CONSTANT } from '../../_constants/text.constant';
import { IFuncionario } from '../../_interfaces/funcionario.interface';
import { ICargo } from '../../_interfaces/cargo.interface';
import { FuncionarioService } from '../funcionario.service';
import { CargoService } from '../../cargo/cargo.service';
import { IPessoa } from '../../_interfaces/pessoa.interface';
import { PessoaService } from '../../pessoa/pessoa.service';

@Component({
    selector: 'app-funcionario-dados',
    templateUrl: './funcionario.component.html',
    styleUrls: ['./funcionario.component.scss']
})
export class FuncionarioDadosComponent implements OnInit {
    constructor(private router: Router,
        private route: ActivatedRoute,
        private funcionarioService: FuncionarioService,
        private cargoService: CargoService,
        private pessoaService: PessoaService,
        private toastService: ToastService) { }

    entity: IFuncionario = {};
    cargos: SelectItem[] = [];
    pessoas: SelectItem[] = [];
    allCargos: ICargo[] = [];
    allPessoas: IPessoa[] = [];
    cols: any;
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
        this.getCargos();
        this.getPessoas();
    }

    get(id: number) {
        this.funcionarioService.get(id)
            .then(response => {
                this.toastService.show(response)
                if (response.status === Status.Success) {
                    this.entity = response.data
                }
            });
    }

    getCargos() {
        this.cargoService.getAll()
            .then(response => {
                this.toastService.show(response)
                if (response.status === Status.Success) {
                    this.allCargos = [];
                    this.cargos = [];
                    this.allCargos = response.data;
                    this.allCargos.forEach(x => {
                        this.cargos.push({ label: x.setor.descricao + " - " + x.descricao, value: x.id });
                    });
                }
            });
    }

    getPessoas() {
        this.pessoaService.getAll()
            .then(response => {
                this.toastService.show(response)
                if (response.status === Status.Success) {
                    this.allPessoas = [];
                    this.pessoas = [];
                    this.allPessoas = response.data;
                    this.allPessoas.forEach(x => {
                        this.pessoas.push({ label: x.id + " - " + x.nome, value: x.id });
                    });
                }
            });
    }

    save() {
        this.entity.salario = Number(this.entity.salario.toString().replace(new RegExp(`^(.{${this.entity.salario.toString().length - 2}})(.)`), `$1${'.'}$2`));
        if (!this.entity) {
            let request: RequestResult = { status: Status.Info, messages: ["Informe todos os dados"] };
            this.toastService.show(request);
        } else {
            this.funcionarioService.save(this.entity)
                .then(response => {
                    if (response.status === Status.Success) {
                        this.router.navigate(['funcionario/index']);
                    }
                    this.toastService.show(response);
                });
        }
    }

    indexPage() {
        this.router.navigate(['funcionario/index']);
    }
}
