import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { add, differenceInCalendarMonths } from 'date-fns';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  constructor(private notificationService : NotificationService) { }

  // public FormatError(err: HttpErrorResponse){
  //   let errorMsg = "";
  //   switch (err.status){
  //     case 400: // Validation Error
  //       Object.keys(err.error.errors).forEach(key => {
  //         err.error.errors[key].forEach((msg: string) => errorMsg += "• " + msg + "\r\n");
  //       });
  //       this.notificationService.ShowError("Errores encontrados", errorMsg);
  //       break;
  //   }
  // }

  // public AddToDate(date: Date, ammount: number, whatToAdd: string) {
  //   let duration = {};
  //   (duration as any)[whatToAdd] = ammount;
  //   return add(date, duration);
  // }

  // public DifferenceInMonths(date1: Date, date2: Date) {
  //   return differenceInCalendarMonths(date2,date1);
  // }

  public GetClass(text: string): string {
    if(text)
      switch(text) {
        // case NombresEstadoIngreso.Facturado:
        //   return 'facturado';
        default:
          return ""
      }
    else
      return "";
  }
}