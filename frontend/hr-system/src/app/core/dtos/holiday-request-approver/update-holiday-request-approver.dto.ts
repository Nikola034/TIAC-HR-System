import { HolidayRequestStatus } from "../../models/holiday-request.model"

export interface UpdateHolidayRequestApproverDto{
    requestId: string,
    approverId: string
    status: HolidayRequestStatus
}