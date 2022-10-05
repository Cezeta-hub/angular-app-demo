import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
@Component({
  selector: 'page-container',
  templateUrl: './page-container.component.html',
  styleUrls: ['./page-container.component.css'],
  host: {class: 'page'}
})
export class PageContainerComponent implements OnInit, OnChanges {

  @Input() title: string = "";
  @Input() loadingText: string = "";
  @Input() loading: boolean = false;
  constructor(private spinner: NgxSpinnerService) { }

  ngOnInit(): void { }
  
  ngOnChanges() {
    if(this.loading)
      this.spinner.show('primary');
    else
      this.spinner.hide('primary');
  }

}
