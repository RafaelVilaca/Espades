import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['/home.component.scss']
})
export class HomeComponent {
    constructor(private route: Router) { }

    pessoaIndex() {
        this.route.navigate(["pessoa/index"]);
    }

    estoqueIndex() {
        this.route.navigate(["estoque/index"]);
    }

    produtoIndex() {
        this.route.navigate(["produto/index"]);
    }

    patrimonioIndex() {
        this.route.navigate(["patrimonio/index"]);
    }

    despesaIndex() {
        this.route.navigate(["despesa/index"]);
    }

    setorIndex() {
        this.route.navigate(["setor/index"]);
    }

    funcionarioIndex() {
        this.route.navigate(["funcionario/index"]);
    }

    clienteIndex() {
        this.route.navigate(["cliente/index"]);
    }

    reservaIndex() {
        this.route.navigate(["reserva/index"]);
    }
}
