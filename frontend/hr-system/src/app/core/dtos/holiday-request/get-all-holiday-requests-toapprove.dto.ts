import { HolidayRequest } from "../../models/holiday-request.model";

export interface GetAllHolidayRequestsToApproveDto{
    holidayRequests: HolidayRequest[],
}