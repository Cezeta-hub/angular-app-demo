import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'loader-wrapper',
  templateUrl: './loader-wrapper.component.html',
  styleUrls: ['./loader-wrapper.component.css'],
  providers: [NgxSpinnerService]
})
export class LoaderWrapperComponent implements OnInit, OnChanges {

  @Input() loadingText: string = "";
  @Input() loading: boolean = false;
  @Input() fullScreen: boolean = false;
  
  constructor(private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
  }
  ngOnChanges() {
    if(this.loading)
      this.spinner.show('secondary');
    else
      this.spinner.hide('secondary');
  }

}
