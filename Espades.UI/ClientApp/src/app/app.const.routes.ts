import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { Routes } from '@angular/router';
import { PessoaComponent } from './pessoa/pessoa.index.component';
import { ProdutoComponent } from './produto/produto.index.component';
import { PatrimonioComponent } from './patrimonio/patrimonio.index.component';
import { ClienteComponent } from './cliente/cliente.index.component';
import { EstoqueComponent } from './estoque/estoque.index.component';
import { DespesaComponent } from './despesa/despesa.index.component';
import { FuncionarioComponent } from './funcionario/funcionario.index.component';
import { ReservaComponent } from './reserva/reserva.index.component';
import { SetorComponent } from './setor/setor.index.component';
import { PessoaDadosComponent } from './pessoa/pessoa-dados/pessoa.component';
import { MasterComponent } from './_master/master.component';
import { ProdutoDadosComponent } from './produto/produto-dados/produto.component';
import { SetorDadosComponent } from './setor/setor-dados/setor.component';
import { FuncionarioDadosComponent } from './funcionario/funcionario-dados/funcionario.component';
import { PatrimonioDadosComponent } from './patrimonio/patrimonio-dados/patrimonio.component';
import { ReservaDadosComponent } from './reserva/reserva-dados/reserva.component';
import { EstoqueDadosComponent } from './estoque/estoque-dados/estoque.component';
import { DespesaDadosComponent } from './despesa/despesa-dados/despesa.component';
import { ClienteDadosComponent } from './cliente/cliente-dados/cliente.component';

export const APP_CONST_ROUTES: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'home', component: HomeComponent },
    {
        path: 'pessoa', component: MasterComponent, data: { url: 'index' },
        children: [
            { path: 'index', component: PessoaComponent },
            { path: 'create', component: PessoaDadosComponent },
            { path: 'edit/:id', component: PessoaDadosComponent }
        ]
    },
    {
        path: 'produto', component: MasterComponent, data: { url: 'index' },
        children: [
            { path: 'index', component: ProdutoComponent },
            { path: 'create', component: ProdutoDadosComponent },
            { path: 'edit/:id', component: ProdutoDadosComponent }
        ]
    },
    {
        path: 'setor', component: MasterComponent, data: { url: 'index' },
        children: [
            { path: 'index', component: SetorComponent },
            { path: 'create', component: SetorDadosComponent },
            { path: 'edit/:id', component: SetorDadosComponent }
        ]
    },
    {
        path: 'patrimonio', component: MasterComponent, data: { url: 'index' },
        children: [
            { path: 'index', component: PatrimonioComponent },
            { path: 'create', component: PatrimonioDadosComponent },
            { path: 'edit/:id', component: PatrimonioDadosComponent }
        ]
    },
    {
        path: 'cliente', component: MasterComponent, data: { url: 'index' },
        children: [
            { path: 'index', component: ClienteComponent },
            { path: 'create', component: ClienteDadosComponent },
            { path: 'edit/:id', component: ClienteDadosComponent }
        ]
    },
    {
        path: 'despesa', component: MasterComponent, data: { url: 'index' },
        children: [
            { path: 'index', component: DespesaComponent },
            { path: 'create', component: DespesaDadosComponent },
            { path: 'edit/:id', component: DespesaDadosComponent }
        ]
    },
    {
        path: 'estoque', component: MasterComponent, data: { url: 'index' },
        children: [
            { path: 'index', component: EstoqueComponent },
            { path: 'create', component: EstoqueDadosComponent },
            { path: 'edit/:id', component: EstoqueDadosComponent }
        ]
    },
    {
        path: 'funcionario', component: MasterComponent, data: { url: 'index' },
        children: [
            { path: 'index', component: FuncionarioComponent },
            { path: 'create', component: FuncionarioDadosComponent },
            { path: 'edit/:id', component: FuncionarioDadosComponent }
        ]
    },
    {
        path: 'reserva', component: MasterComponent, data: { url: 'index' },
        children: [
            { path: 'index', component: ReservaComponent },
            { path: 'create', component: ReservaDadosComponent },
            { path: 'edit/:id', component: ReservaDadosComponent }
        ]
    }
];
