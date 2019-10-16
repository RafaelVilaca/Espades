import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-endereco',
  templateUrl: './endereco.index.component.html',
  styleUrls: ['./endereco.index.component.scss']
})
export class EnderecoComponent implements OnInit {
  constructor(private route: Router) { }

  ngOnInit() {
  }
}
