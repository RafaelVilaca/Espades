import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.index.component.html',
  styleUrls: ['./cliente.index.component.scss']
})
export class ClienteComponent implements OnInit {
  constructor(private route: Router) { }

  ngOnInit() {
  }
}
