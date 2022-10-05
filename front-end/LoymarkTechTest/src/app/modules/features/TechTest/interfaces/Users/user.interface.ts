import { History } from "../History/history.interface"

export interface User {
  // Basic Info
  Id : number,
  Name: string,
  Surname: string,
  Birthday: string,

  // Contact Info
  Email: string,
  Telephone?: number,
  CountryId?: number,
  Country?: Country,

  // Extra
  WishesToBeContacted: boolean,

  // Tracking metadata
  ChangeHistory: History[]
}

export interface Country {
  Id: number,
  Name: string,
  Code: string,
  Value: number
}

// --- API request interfaces --- //
export interface UserPayload {
  Id? : string,
  Name: string,
  Surname: string,
  Birthday: Date,
  Email: string,
  Telephone: number,
  CountryCode: string,
  WishesToBeContacted: boolean
}
export interface UsersSearchPayload {
  Name?: string,
  OrderBy: string,
  Direction: number,
  Page: number,
  PageSize: number
}

// // -- API response interfaces -- //
export interface IdResult {
  Id: number
}
export interface CountryListResult {
  Result: {
    Id: number,
    Name: string,
    Code: string,
    Value: number
  }[]
}
