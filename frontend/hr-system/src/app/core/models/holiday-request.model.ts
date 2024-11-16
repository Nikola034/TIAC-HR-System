import { Employee } from "./employee.model";

export interface HolidayRequest{
    id: string,
    start: Date,
    end: Date,
    sender: Employee,
    status: HolidayRequestStatus
}

export enum HolidayRequestStatus{ 
    Approved, 
    Pending,
    Denied
}
