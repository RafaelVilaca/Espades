import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-produto',
  templateUrl: './produto.index.component.html',
  styleUrls: ['./produto.index.component.scss']
})
export class ProdutoComponent implements OnInit {
  constructor(private route: Router) { }

  ngOnInit() {
  }
}
