import { HolidayRequest } from "../../models/holiday-request.model";

export interface GetAllHolidayRequestsBySenderDto{
    holidayRequests: HolidayRequest[]
    totalPages: number,
    page: number,
    itemsPerPage : number,
}