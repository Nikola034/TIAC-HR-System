import { HolidayRequest } from "../../models/holiday-request.model";

export interface GetAllHolidayRequestsDto{
    holidayRequests: HolidayRequest[],
    totalPages: number,
    page: number,
    itemsPerPage : number,
}