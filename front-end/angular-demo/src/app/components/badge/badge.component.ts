import { Component, Input, OnInit } from '@angular/core';
import { HelperService } from 'src/app/globalServices/helper.service';

@Component({
  selector: 'badge',
  template: `
    <p-tag [value]="text" [icon]="iconName" [ngClass]="classNames"></p-tag>
  `
})
export class BadgeComponent implements OnInit {
  public classNames: string = "";
  public iconName: string = "";
  
  @Input() classes?: string = "";
  @Input() text: string = "";
  @Input() icon?: string = "";
  
  constructor(private helperService: HelperService) { }

  ngOnInit(): void {
    if(!this.classes) {
      this.classNames = this.helperService.GetClass(this.text);
    }

    if(this.icon)
      this.iconName = this.icon;
  }

}
