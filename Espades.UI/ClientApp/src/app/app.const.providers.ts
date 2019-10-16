import { CurrencyPipe, DatePipe, DecimalPipe, PercentPipe } from '@angular/common';
import { DialogService, MessageService } from 'primeng/api';
import { CustomHttpClient } from './_common/custom-http-client';
import { ToastService } from './toast-component/toast.service';
import { LoginService } from './login/login.service';
import { SessionConfig, SESSION_CONFIG } from './session.config';
import { PessoaService } from './pessoa/pessoa.service';
import { ProdutoService } from './produto/produto.service';
import { PatrimonioService } from './patrimonio/patrimonio.service';
import { EnderecoService } from './endereco/endereco.service';
import { CargoService } from './cargo/cargo.service';
import { ClienteService } from './cliente/cliente.service';
import { EstoqueService } from './estoque/estoque.service';
import { DespesaService } from './despesa/despesa.service';
import { FuncionarioService } from './funcionario/funcionario.service';
import { ReservaService } from './reserva/reserva.service';
import { SetorService } from './setor/setor.service';

export const APP_CONST_PROVIDERS = [
  CurrencyPipe,
  DatePipe,
  DecimalPipe,
  PercentPipe,
  CustomHttpClient,
  LoginService,
  MessageService,
  ToastService,
  DialogService,
  PessoaService,
  ProdutoService,
  PatrimonioService,
  EnderecoService,
  CargoService,
  ClienteService,
  DespesaService,
  EstoqueService,
  FuncionarioService,
  SetorService,
  ReservaService,
  { provide: SESSION_CONFIG, useValue: SessionConfig }
];
