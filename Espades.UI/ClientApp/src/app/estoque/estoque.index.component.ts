import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-estoque',
  templateUrl: './estoque.index.component.html',
  styleUrls: ['./estoque.index.component.scss']
})
export class EstoqueComponent implements OnInit {
  constructor(private route: Router) { }

  ngOnInit() {
  }
}
