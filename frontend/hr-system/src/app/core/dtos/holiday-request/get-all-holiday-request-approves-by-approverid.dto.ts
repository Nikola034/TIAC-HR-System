import { HolidayRequestApprover } from "../../models/holiday-request-approver.model.ts";
import { HolidayRequestStatus } from "../../models/holiday-request.model.js";

export interface GetAllHolidayRequestApproversByApproverIdDto{
    id: string,
    requestId: string,
    senderName: string,
    senderSurname: string,
    start: Date,
    end: Date,
    status: HolidayRequestStatus
}

export interface GetAllHolidayRequestApproversByApproverIdResponse {
    holidayRequestApprovers: GetAllHolidayRequestApproversByApproverIdDto[]
}