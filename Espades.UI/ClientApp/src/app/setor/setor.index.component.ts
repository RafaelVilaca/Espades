import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-setor',
  templateUrl: './setor.index.component.html',
  styleUrls: ['./setor.index.component.scss']
})
export class SetorComponent implements OnInit {
  constructor(private route: Router) { }

  ngOnInit() {
  }
}
