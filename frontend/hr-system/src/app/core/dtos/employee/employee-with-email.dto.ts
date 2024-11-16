import { EmployeeRole } from "../../models/employee.model";

export interface EmployeeWithEmailDto{
    id: string
    name: string,
    surname: string,
    email: string,
    daysOff: number,
    role: EmployeeRole,
    accountId: string
}