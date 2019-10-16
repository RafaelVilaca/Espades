import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-despesa',
  templateUrl: './despesa.index.component.html',
  styleUrls: ['./despesa.index.component.scss']
})
export class DespesaComponent implements OnInit {
  constructor(private route: Router) { }

  ngOnInit() {
  }
}
