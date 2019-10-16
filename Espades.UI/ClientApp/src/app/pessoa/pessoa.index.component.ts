import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pessoa',
  templateUrl: './pessoa.index.component.html',
  styleUrls: ['./pessoa.index.component.scss']
})
export class PessoaComponent implements OnInit {
  constructor(private route: Router) { }

  ngOnInit() {
  }
}
