import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { IProduto } from '../../_interfaces/produto.interface';
import { ProdutoService } from '../produto.service';
import { Status } from '../../_enums/status.enum';
import { ToastService } from '../../toast-component/toast.service';
import { RequestResult } from '../../_interfaces/request-result.interface';
import { TEXT_CONSTANT } from '../../_constants/text.constant';

@Component({
    selector: 'app-produto-dados',
    templateUrl: './produto.component.html',
    styleUrls: ['./produto.component.scss']
})
export class ProdutoDadosComponent implements OnInit {
    constructor(private router: Router,
        private route: ActivatedRoute,
        private toastService: ToastService,
        private produtoService: ProdutoService) { }

    entity: IProduto = {};

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
    }

    get(id: number) {
        this.produtoService.get(id)
            .then(response => {
                this.toastService.show(response)
                if (response.status === Status.Success) {
                    this.entity = response.data
                }
            });
    }

    save() {
        if (!this.entity) {
            let request: RequestResult = { status: Status.Info, messages: ["Informe todos os dados"] };
            this.toastService.show(request);
        } else {
            this.produtoService.save(this.entity)
                .then(response => {
                    if (response.status === Status.Success) {
                        this.router.navigate(['produto/index']);
                    }
                    this.toastService.show(response);
                });
        }
    }

    indexPage() {
        this.router.navigate(['produto/index']);
    }
}
