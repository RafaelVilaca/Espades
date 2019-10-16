import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-funcionario',
  templateUrl: './funcionario.index.component.html',
  styleUrls: ['./funcionario.index.component.scss']
})
export class FuncionarioComponent implements OnInit {
  constructor(private route: Router) { }

  ngOnInit() {
  }
}
