import { EmployeeRole } from "../../models/employee.model";

export interface UpdateEmployeeDto{
    id: string,
    name: string,
    surname: string,
    daysOff: number,
    role: EmployeeRole
}