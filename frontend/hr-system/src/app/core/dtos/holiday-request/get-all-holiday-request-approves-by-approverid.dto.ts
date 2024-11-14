import { HolidayRequestApprover } from "../../models/holiday-request-approver.model.ts";

export interface GetAllHolidayRequestApproversByApproverIdDto{
    holidayRequestApprovers: HolidayRequestApprover[],
}