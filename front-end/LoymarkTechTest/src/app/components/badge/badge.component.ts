import { Component, Input, OnInit } from '@angular/core';
import { HelperService } from 'src/app/globalServices/helper.service';

@Component({
  selector: 'badge',
  templateUrl: './badge.component.html',
  styleUrls: ['./badge.component.css']
})
export class BadgeComponent implements OnInit {

  constructor(private helperService: HelperService) { }
  @Input() classes?: string = "";
  @Input() text: string = "";
  @Input() icon?: string = "";
  public classNames: string = "";
  public iconName: string = "";
  ngOnInit(): void {
    if(!this.classes) {
      this.classNames = this.helperService.GetClass(this.text);
    }

    if(this.icon)
      this.iconName = this.icon;
  }

}
