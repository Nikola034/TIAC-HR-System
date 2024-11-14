import { HolidayRequestStatus } from "../../models/holiday-request.model"

export interface CreateHolidayRequestDto{
    start: Date | undefined | null,
    end: Date | undefined | null,
    senderId: string
    status: HolidayRequestStatus.Pending
}