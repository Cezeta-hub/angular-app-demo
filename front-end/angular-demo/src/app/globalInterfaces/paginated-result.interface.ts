export interface PaginatedResult<T> {
    CurrentPage: number,
    CurrentAmountOfObjects: number,
    TotalObjects: number,
    TotalPages: number,
    TotalSeleccionableObjects: number,
    Result: T[]
}