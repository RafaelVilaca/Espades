import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cargo',
  templateUrl: './cargo.index.component.html',
  styleUrls: ['./cargo.index.component.scss']
})
export class CargoComponent implements OnInit {
  constructor(private route: Router) { }

  ngOnInit() {
  }
}
