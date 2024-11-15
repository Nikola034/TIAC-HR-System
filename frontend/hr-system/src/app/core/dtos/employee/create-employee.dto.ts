import { EmployeeRole } from "../../models/employee.model";

export interface CreateEmployeeDto{
    name: string,
    surname: string,
    daysOff: number,
    role: EmployeeRole,
    accountId: string
}