import { Employee } from "../../models/employee.model";

export interface AllEmployeesDto{
    map(arg0: (emp: any) => any): unknown;
    employees : Employee[],
    totalPages: number,
    page: number,
    itemsPerPage : number,
}