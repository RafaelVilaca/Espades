import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProdutoService } from './produto.service';
import { ToastService } from '../toast-component/toast.service';
import { IProduto } from '../_interfaces/produto.interface';
import { Status } from '../_enums/status.enum';

@Component({
    selector: 'app-produto',
    templateUrl: './produto.index.component.html',
    styleUrls: ['./produto.index.component.scss']
})
export class ProdutoComponent implements OnInit {
    constructor(private route: Router, private produtoService: ProdutoService, private toastService: ToastService) { }

    entity: IProduto = null;

    ngOnInit() {
        this.get();
    }

    addProduto() {
        this.route.navigate(["produto/create"]);
    }

    editarEntity(e: number) {
        this.route.navigate(["produto/edit/" + e]);
    }

    excluirEntity(e: number) {
        this.toastService.showConfirm("Deseja realmente deletar este registro?", "Remoção de registro",
            "Sim", "Não", Status.Warning, this.onConfirmDelete.bind(this, e));
    }

    onConfirmDelete(entityId: number) {
        this.produtoService.delete(entityId)
            .then(response => {
                this.toastService.show(response);
                this.get();
            })
    }

    get() {
        this.produtoService.getAll()
            .then(response => {
                if (response.status === Status.Success) {
                    this.entity = response.data;
                }
            });
    }
}
