import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { Routes } from '@angular/router';
import { PessoaComponent } from './pessoa/pessoa.index.component';
import { ProdutoComponent } from './produto/produto.index.component';
import { PatrimonioComponent } from './patrimonio/patrimonio.index.component';
import { EnderecoComponent } from './endereco/endereco.index.component';
import { CargoComponent } from './cargo/cargo.index.component';
import { ClienteComponent } from './cliente/cliente.index.component';
import { EstoqueComponent } from './estoque/estoque.index.component';
import { DespesaComponent } from './despesa/despesa.index.component';
import { FuncionarioComponent } from './funcionario/funcionario.index.component';
import { ReservaComponent } from './reserva/reserva.index.component';
import { SetorComponent } from './setor/setor.index.component';

export const APP_CONST_ROUTES: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'pessoa', component: PessoaComponent },
  { path: 'produto', component: ProdutoComponent },
  { path: 'patrimonio', component: PatrimonioComponent },
  { path: 'endereco', component: EnderecoComponent },
  { path: 'cargo', component: CargoComponent },
  { path: 'cliente', component: ClienteComponent },
  { path: 'despesa', component: DespesaComponent },
  { path: 'estoque', component: EstoqueComponent },
  { path: 'funcionario', component: FuncionarioComponent },
  { path: 'setor', component: SetorComponent },
  { path: 'reserva', component: ReservaComponent },
];
