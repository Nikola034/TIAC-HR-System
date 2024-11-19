import { HolidayRequestApprover } from "../../models/holiday-request-approver.model.ts";

export interface GetAllHolidayRequestApproversByApproverIdDto{
    id: string,
    requestId: string,
    senderName: string,
    senderSurname: string,
    start: Date,
    end: Date
}

export interface GetAllHolidayRequestApproversByApproverIdResponse {
    holidayRequestApprovers: GetAllHolidayRequestApproversByApproverIdDto[]
}