import { HolidayRequestStatus } from "../../models/holiday-request.model"

export interface UpdateHolidayRequestApproverDto{
    id: string,
    requestId: string,
    approverId: string
    status: HolidayRequestStatus
}