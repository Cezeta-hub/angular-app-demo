import { ChangeTypeEnum } from "src/app/enums/change-type.enum";

export interface History {
  Id : number,
  ChangeType: ChangeTypeEnum,
  PrevValue: string,
  CurrValue: string,
  ChangeDate: Date,
  UserId: number
}
