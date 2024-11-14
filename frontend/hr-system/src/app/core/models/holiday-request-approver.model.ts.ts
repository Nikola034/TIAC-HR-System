import { HolidayRequestStatus } from "./holiday-request.model";

export interface HolidayRequestApprover{
    id: string,
    requestId: string,
    approverId: string,
    status: HolidayRequestStatus
}

