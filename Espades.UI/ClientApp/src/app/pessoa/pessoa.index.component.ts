import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IPessoa } from '../_interfaces/pessoa.interface';
import { PessoaService } from './pessoa.service';
import { Status } from '../_enums/status.enum';
import { ToastService } from '../toast-component/toast.service';

@Component({
  selector: 'app-pessoa',
  templateUrl: './pessoa.index.component.html',
  styleUrls: ['./pessoa.index.component.scss']
})
export class PessoaComponent implements OnInit {
  constructor(private route: Router, private pessoaService: PessoaService, private toastService: ToastService) { }

  entity: IPessoa[] = [];
  cols: any;

  ngOnInit() {
    this.getPessoas();
  }

  addPessoa() {
    this.route.navigate(["pessoa/create"]);
  }

  editarEntity(e: number) {
    this.route.navigate(["pessoa/edit/" + e]);
  }

  excluirEntity(e: number) {
    this.toastService.showConfirm("Deseja realmente deletar este registro?", "Remoção de registro",
      "Sim", "Não", Status.Warning, this.onConfirmDelete.bind(this, e));
  }

  onConfirmDelete(entityId: number) {
    this.pessoaService.delete(entityId)
      .then(response => {
        this.toastService.show(response);
        this.getPessoas();
      })
  }

  getPessoas() {
    this.pessoaService.getAll()
      .then(response => {
        if (response.status === Status.Success) {
          this.entity = response.data;
        }
      });
  }
}
