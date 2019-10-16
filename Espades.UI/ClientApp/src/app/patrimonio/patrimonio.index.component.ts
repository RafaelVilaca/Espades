import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patrimonio',
  templateUrl: './patrimonio.index.component.html',
  styleUrls: ['./patrimonio.index.component.scss']
})
export class PatrimonioComponent implements OnInit {
  constructor(private route: Router) { }

  ngOnInit() {
  }
}
