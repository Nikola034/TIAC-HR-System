import { Employee } from "../../models/employee.model";

export interface DaysOffReportDto{
    employee: Employee,
    realizedDays: number,
    remainingDays: number,
    pendingHolidayRequests: number
}