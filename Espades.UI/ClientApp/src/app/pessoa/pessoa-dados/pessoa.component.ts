import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { IPessoa } from '../../_interfaces/pessoa.interface';
import { PessoaService } from '../pessoa.service';
import { Status } from '../../_enums/status.enum';
import { ToastService } from '../../toast-component/toast.service';
import { RequestResult } from '../../_interfaces/request-result.interface';
import { TabOptions } from '../../_enums/tab-options.enum';
import { SelectItem } from 'primeng/api';
import { EnderecoService } from '../../endereco/endereco.service';
import { IEndereco } from '../../_interfaces/endereco.interface';
import { TEXT_CONSTANT } from '../../_constants/text.constant';

@Component({
  selector: 'app-pessoa-dados',
  templateUrl: './pessoa.component.html',
  styleUrls: ['./pessoa.component.scss']
})
export class PessoaDadosComponent implements OnInit {
  constructor(private router: Router,
    private route: ActivatedRoute,
    private toastService: ToastService,
    private enderecoService: EnderecoService,
    private pessoaService: PessoaService) { }

  entity: IPessoa = {};
  cols: any;
  confirmSenha: string = "";
  optionButton: any;
  oldOptionButton: any;
  types: SelectItem[] = [];
  endereco: IEndereco = {};
  sexos: SelectItem[] = [];

  textConstant = TEXT_CONSTANT;

  ngOnInit() {
    this.route.paramMap.subscribe((data: any) => {
      if (data.params !== undefined && data.params.id !== undefined) {
        let uId = data.params.id;
        if (uId != null && uId > 0) {
          this.getPessoa(uId);
        }
      }
    });

    this.types = [
      { label: 'Dados', value: TabOptions.Data, icon: 'pi pi-users' },
      { label: 'Endereços', value: TabOptions.Endereco, icon: 'pi pi-home' }
    ];

    this.sexos = [
      { label: 'Masculino', value: "M" },
      { label: 'Feminino', value: "F" },
      { label: 'Outro', value: "O" }
    ];

    this.optionButton = TabOptions.Data;
    this.oldOptionButton = TabOptions.Data;
  }

  getPessoa(id: number) {
    this.pessoaService.get(id)
      .then(response => {
        this.toastService.show(response)
        if (response.status === Status.Success) {
          this.entity = response.data
        }
      });
  }

  onChangeOption() {
    if (this.oldOptionButton === TabOptions.Data && this.optionButton !== TabOptions.Data) {
      this.savePessoa(false);
    } else {
      this.allowTabChange();
    }

    if (this.entity.id && this.entity.id > 0) {
      if (this.optionButton === TabOptions.Endereco) {
        //this.getEnderecos();
      }
    }
  }

  allowTabChange() {
    this.oldOptionButton = this.optionButton;
  }

  savePessoa(redirect: boolean) {
    if (!this.entity) {
      let request: RequestResult = { status: Status.Info, messages: ["Informe todos os dados"] };
      this.toastService.show(request);
      this.optionButton = TabOptions.Data;
    } else if (this.entity.senha === "" || this.confirmSenha === "" || this.confirmSenha !== this.entity.senha) {
      let request: RequestResult = { status: Status.Warning, messages: ["As senhas não coincidem"] };
      this.toastService.show(request);
      this.optionButton = TabOptions.Data;
    } else {
      this.pessoaService.save(this.entity)
        .then(response => {
          if (response.status === Status.Success) {
            if (redirect) {
              this.router.navigate(['pessoa/index']);
            }
          }
          this.toastService.show(response);
        });
    }
  }

  saveEndereco() {
    this.endereco.id_Pessoa = this.entity.id;
    if (!this.endereco) {
      let request: RequestResult = { status: Status.Info, messages: ["Informe todos os dados do endereço"] };
      this.toastService.show(request);
    } else {
      this.enderecoService.save(this.endereco)
        .then(response => {
          if (response.status === Status.Success) {
            this.getPessoa(this.entity.id);
          }
          this.toastService.show(response);
        });
    }
  }

  editarEndereco(e: number) {
    this.enderecoService.get(e)
      .then(response => {
        if (response.status === Status.Success) {
          this.getPessoa(this.entity.id);
        }
        this.toastService.show(response);
      });
  }

  excluirEndereco(e: number) {
    this.toastService.showConfirm("Deseja realmente deletar este registro?", "Remoção de registro",
      "Sim", "Não", Status.Warning, this.onConfirmDelete.bind(this, e));
  }

  onConfirmDelete(entityId: number) {
    this.pessoaService.delete(entityId)
      .then(response => {
        this.toastService.show(response);
        this.getPessoa(this.entity.id)
      });
  }

  indexPage() {
    this.router.navigate(['pessoa/index']);
  }
}
