import { ChangeTypeEnum } from "src/app/enums/change-type.enum";

export interface History {
  Id : number,
  ChangeType: ChangeTypeEnum,
  PrevValue: string,
  CurrValue: string,
  ChangeDate: Date,
  UserId: number
}

// --- API request interfaces --- //
export interface HistorySearchPayload {
  UserId?: string,
  OrderBy: string,
  Direction: number,
  Page: number,
  PageSize: number
}

// // -- API response interfaces -- //
export interface IdResult {
  Id: number
}
export interface ChangeTypeListResult {
  Result: {
    Id: number,
    Name: string,
  }[]
}