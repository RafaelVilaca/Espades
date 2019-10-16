import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reserva',
  templateUrl: './reserva.index.component.html',
  styleUrls: ['./reserva.index.component.scss']
})
export class ReservaComponent implements OnInit {
  constructor(private route: Router) { }

  ngOnInit() {
  }
}
