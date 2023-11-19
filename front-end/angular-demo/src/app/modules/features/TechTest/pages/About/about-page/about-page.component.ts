import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'about',
  templateUrl: './about-page.component.html',
  styleUrls: ['./about-page.component.css'],
})
export class AboutPageComponent implements OnInit {

  private loading: boolean = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit(): void {}
}